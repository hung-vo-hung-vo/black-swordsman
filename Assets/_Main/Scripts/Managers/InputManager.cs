using UnityEngine;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] KeyCode left = KeyCode.A;
    [SerializeField] KeyCode right = KeyCode.D;
    [SerializeField] KeyCode jump = KeyCode.Space;
    [SerializeField] KeyCode attack0 = KeyCode.J;
    [SerializeField] KeyCode attack1 = KeyCode.K;
    [SerializeField] KeyCode attack2 = KeyCode.L;
    [SerializeField] KeyCode heal = KeyCode.T;

    public UnityEvent<int> OnRun { get; private set; }
    public UnityEvent OnJump { get; private set; }
    public UnityEvent<int> OnAttack { get; private set; }
    public UnityEvent<float> OnHeal { get; private set; }

    private void Update()
    {
        Run();
        Jump();
        Attack();
        Heal();
    }

    void Run()
    {
        if (Input.GetKey(left) && Input.GetKey(right))
        {
            OnRun?.Invoke(0);
        }
        else if (Input.GetKey(left))
        {
            OnRun?.Invoke(-1);
        }
        else if (Input.GetKey(right))
        {
            OnRun?.Invoke(1);
        }
        else
        {
            OnRun?.Invoke(0);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(jump))
        {
            OnJump?.Invoke();
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(attack0))
        {
            OnAttack?.Invoke(0);
        }
        else if (Input.GetKeyDown(attack1))
        {
            OnAttack?.Invoke(1);
        }
        else if (Input.GetKeyDown(attack2))
        {
            OnAttack?.Invoke(2);
        }
    }

    void Heal()
    {
        if (Input.GetKeyDown(heal))
        {
            OnHeal?.Invoke(0f);
        }
    }
}