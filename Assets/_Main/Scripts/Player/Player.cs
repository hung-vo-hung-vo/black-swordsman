using System.Collections.Generic;
using Cinemachine;
using FishNet.Object;
using UnityEngine;
using UnityEngine.Events;

public class Player : ApcsNetworkBehaviour
{
    [SerializeField] PlayerDataSO _playerData;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _body;
    [SerializeField] SpriteRenderer _avatar;
    [SerializeField] SkillAgent _skillAgent;
    [SerializeField] JumpPlayer _jumper;
    [SerializeField] StatAgent _stat;
    [SerializeField] Inventory _inventory;

    public override void OnStartClient()
    {
        base.OnStartClient();
        IfIsOwnerThenDo(() =>
        {
            _stat.Init(_playerData);
            _skillAgent.Init(_stat, _playerData.GetSkills());
            _jumper.Init(_stat, _playerData, Jump);
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

    public void Run(int dir)
    {
        _animator.SetBool(AnimationParam.Run, dir != 0);

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
        _animator.SetTrigger(AnimationParam.Jump);
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
                _animator.SetTrigger(AnimationParam.Death);
                IfIsOwnerThenDo(UnsubscribeInput);
            }
            else
            {
                _animator.SetTrigger(AnimationParam.TakeHit);
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
}