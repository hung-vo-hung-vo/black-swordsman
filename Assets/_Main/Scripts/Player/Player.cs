using System.Collections.Generic;
using Cinemachine;
using FishNet.Object;
using UnityEngine;

public class Player : ApcsNetworkBehaviour
{
    [SerializeField] PlayerDataSO _playerData;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _body;
    [SerializeField] SpriteRenderer _avatar;
    [SerializeField] SkillAgent _skillAgent;
    [SerializeField] JumpPlayer _jumper;

    public float HealthPoint { get; private set; }

    public override void OnStartClient()
    {
        base.OnStartClient();
        HealthPoint = _playerData.MaxHealthPoint;
        _skillAgent.SetSkills(_playerData.GetSkills());
        _jumper.Init(_playerData, Jump);
        IfIsOwnerThenDo(() =>
        {
            RegisterInput();
            VirtualCameraFollow();
        });
    }

    void RegisterInput()
    {
        InputManager.Instance.OnRun.AddListener(Run);
        InputManager.Instance.OnJump.AddListener(_jumper.Do);
        InputManager.Instance.OnAttack.AddListener(Attack);
        InputManager.Instance.OnHeal.AddListener(Heal);
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

    public void TakeHit(float damage)
    {
        var healthPoint = Mathf.Clamp(HealthPoint - damage, 0, _playerData.MaxHealthPoint);
        if (healthPoint != HealthPoint)
        {
            HealthPoint = healthPoint;
            if (HealthPoint <= 0)
            {
                _animator.SetTrigger(AnimationParam.Death);
            }
            else
            {
                _animator.SetTrigger(AnimationParam.TakeHit);
            }
        }
    }

    public void Heal(float heal)
    {
        var healthPoint = Mathf.Clamp(HealthPoint + heal, 0, _playerData.MaxHealthPoint);
        if (healthPoint != HealthPoint)
        {
            HealthPoint = healthPoint;
        }
    }
}