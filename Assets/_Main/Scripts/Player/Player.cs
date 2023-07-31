using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FishNet;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;
using UnityEngine.Events;

public class Player : ApcsNetworkBehaviour, IHealthable
{
    [SerializeField] PlayerDataSO _playerData;
    [SerializeField] AnimationAgent _anim;
    [SerializeField] Rigidbody2D _body;
    [SerializeField] SpriteRenderer _avatar;
    [SerializeField] SkillAgent _skillAgent;
    [SerializeField] JumpPlayer _jumper;
    [SerializeField] StatAgent _stat;
    [SerializeField] Inventory _inventory;

    public UnityEvent OnDie() => _onDie;
    public UnityEvent OnTakeDamage() => _onTakeDamage;
    public bool IsDead() => _isDead;

    UnityEvent _onDie = new UnityEvent();
    UnityEvent _onTakeDamage = new UnityEvent();
    bool _isDead = false;

    public override void OnStartClient()
    {
        base.OnStartClient();
        IfIsOwnerThenDo(() =>
        {
            _stat.Init(_playerData);
            _skillAgent.Init(_stat, _playerData.GetSkills());
            _jumper.Init(_stat, _playerData, Jump);
            _inventory.SetupUI();

            OnDie().AddListener(() => _isDead = true);
            OnDie().AddListener(UnsubscribeInput);
            OnDie().AddListener(_body.Sleep);
            OnDie().AddListener(() => _anim.SetTrigger(AnimationParam.Death));

            OnTakeDamage().AddListener(() => _anim.SetTrigger(AnimationParam.TakeHit));
            OnTakeDamage().AddListener(() => StartCoroutine(IEShock()));

            RegisterInput();
            VirtualCameraFollow();
        });
    }

    void OnDestroy()
    {
        IfIsOwnerThenDo(UnsubscribeInput);
    }

    void RegisterInput()
    {
        InputManager.Instance.OnRun.AddListener(Run);
        InputManager.Instance.OnJump.AddListener(_jumper.Do);
        InputManager.Instance.OnAttack.AddListener(Attack);
        InputManager.Instance.OnUseItem.AddListener(UseItem);
    }

    void UnsubscribeInput()
    {
        InputManager.Instance.OnRun.RemoveListener(Run);
        InputManager.Instance.OnJump.RemoveListener(_jumper.Do);
        InputManager.Instance.OnAttack.RemoveListener(Attack);
        InputManager.Instance.OnUseItem.RemoveListener(UseItem);
    }

    void VirtualCameraFollow()
    {
        var virCam = FindObjectOfType<CinemachineVirtualCamera>();
        virCam.Follow = transform;
    }

    IEnumerator IEShock()
    {
        UnsubscribeInput();

        var knockBack = ((_avatar.flipX ? Vector2.right : Vector2.left) + Vector2.up * 3).normalized * _playerData.ShockForce;
        _body.AddForce(knockBack, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_playerData.ShockTime);
        RegisterInput();
    }

    public void Run(int dir)
    {
        _anim.SetBool(AnimationParam.Run, dir != 0);

        if (dir == 0)
        {
            _body.velocity = new Vector2(0, _body.velocity.y);
            return;
        }

        var normalDir = dir / Mathf.Abs(dir);
        _body.velocity = new Vector2(_playerData.RunSpeed * normalDir, _body.velocity.y);
        _avatar.flipX = normalDir < 0;
        _skillAgent.transform.localScale = new Vector3(normalDir, 1, 1);
    }

    void Jump()
    {
        _anim.SetTrigger(AnimationParam.Jump);
        _body.AddForce(Vector2.up * _playerData.JumpForce, ForceMode2D.Impulse);
    }

    public void Attack(int skill)
    {
        _skillAgent.Attack(skill);
    }

    public void TakeDamage(AttackStats attack)
    {
        if (_stat.UpdateHealth(-attack.damage))
        {
            if (_stat.HealthPoint <= 0)
            {
                _onDie?.Invoke();
            }
            else
            {
                _onTakeDamage?.Invoke();
            }
        }
    }

    public void UseItem(ItemAction action)
    {
        var item = _inventory.GetItemByAction(action);
        if (item != null)
        {
            _inventory.UseItem(item);
            switch (item.Action)
            {
                case ItemAction.HP:
                    _stat.UpdateHealth(item.Number);
                    break;
                case ItemAction.MP:
                    _stat.UpdateMana(item.Number);
                    break;
                case ItemAction.JumpForce:
                    _stat.SetExtraJumpForce(item.Number);
                    break;
                case ItemAction.Damage:
                    _skillAgent.SetExtraDamage(item.Number);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ApcsLayerMask.DEATH))
        {
            TakeDamage(new AttackStats() { damage = _stat.HealthPoint });
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer(ApcsLayerMask.SCENE_LOAD))
        {
            InstanceFinder.ClientManager.StopConnection();
        }
    }

    public override void OnStopClient()
    {
        InstanceFinder.ClientManager.OnClientConnectionState += OnStoryClientConnState;
        GameManager.Instance.ConnectToServer(false);
    }

    void OnStoryClientConnState(ClientConnectionStateArgs args)
    {
        switch (args.ConnectionState)
        {
            case LocalConnectionState.Started:
                InstanceFinder.ClientManager.OnClientConnectionState -= OnStoryClientConnState;
                ApcsSceneLoader.Instance.LoadStoryGame();
                return;
            case LocalConnectionState.Stopping:
                InstanceFinder.ClientManager.OnClientConnectionState -= OnStoryClientConnState;
                break;
        }
    }
}