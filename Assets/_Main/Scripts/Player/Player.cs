using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerDataSO _playerData;

    public float HealthPoint { get; private set; }

    Animator _animator;
    Rigidbody2D _body;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();

        HealthPoint = _playerData.MaxHealthPoint;
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

    public void TakeHit(int damage)
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

    public void Heal(int heal)
    {
        var healthPoint = Mathf.Clamp(HealthPoint + heal, 0, _playerData.MaxHealthPoint);
        if (healthPoint != HealthPoint)
        {
            HealthPoint = healthPoint;
        }
    }
}