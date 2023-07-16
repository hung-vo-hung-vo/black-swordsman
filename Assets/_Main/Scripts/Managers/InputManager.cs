using System.Collections.Generic;
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
    [SerializeField] ItemInput[] _itemInputs;

    public UnityEvent<int> OnRun { get; private set; } = new UnityEvent<int>();
    public UnityEvent OnJump { get; private set; } = new UnityEvent();
    public UnityEvent<int> OnAttack { get; private set; } = new UnityEvent<int>();
    public UnityEvent<ItemAction> OnUseItem { get; private set; } = new UnityEvent<ItemAction>();

    Dictionary<KeyCode, ItemAction> _itemInputDict;

    protected override void Awake()
    {
        base.Awake();
        _itemInputDict = new Dictionary<KeyCode, ItemAction>();
        foreach (var input in _itemInputs)
        {
            _itemInputDict[input.input] = input.action;
        }
    }

    private void Update()
    {
        Run();
        Jump();
        Attack();
        UseItem();
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

    void UseItem()
    {
        foreach (var input in _itemInputDict)
        {
            if (Input.GetKeyDown(input.Key))
            {
                OnUseItem?.Invoke(input.Value);
                return;
            }
        }
    }
}