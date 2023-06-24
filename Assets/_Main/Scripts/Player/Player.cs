using FishNet.Object;
using UnityEngine;

public class Player : ApcsNetworkBehaviour
{
    [SerializeField] PlayerDataSO _playerData;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _body;

    public float HealthPoint { get; private set; }

    public override void OnStartClient()
    {
        base.OnStartClient();
        HealthPoint = _playerData.MaxHealthPoint;
        IfIsOwnerThenDo(RegisterInput);
    }

    void RegisterInput()
    {
        InputManager.Instance.OnRun.AddListener(Run);
        InputManager.Instance.OnJump.AddListener(Jump);
        InputManager.Instance.OnAttack.AddListener(Attack);
        InputManager.Instance.OnHeal.AddListener(Heal);
    }

    public void Run(int dir)
    {
        _animator.SetBool("run", dir != 0);

        if (dir == 0)
        {
            _body.velocity = new Vector2(0, _body.velocity.y);
            return;
        }

        var normalDir = dir / Mathf.Abs(dir);
        _body.velocity = new Vector2(_playerData.RunSpeed * normalDir, _body.velocity.y);
        transform.localScale = new Vector3(normalDir, 1, 1);
    }

    public void Jump()
    {
        _animator.SetTrigger("jump");
        _body.AddForce(Vector2.up * _playerData.JumpForce, ForceMode2D.Impulse);
    }

    public void Attack(int skill)
    {
        _animator.SetTrigger("attack" + skill.ToString());
    }

    public void TakeHit(float damage)
    {
        var healthPoint = Mathf.Clamp(HealthPoint - damage, 0, _playerData.MaxHealthPoint);
        if (healthPoint != HealthPoint)
        {
            HealthPoint = healthPoint;
            if (HealthPoint <= 0)
            {
                _animator.SetTrigger("death");
            }
            else
            {
                _animator.SetTrigger("takeHit");
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