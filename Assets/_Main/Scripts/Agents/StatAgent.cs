using UnityEngine;
using UnityEngine.Events;

public class StatAgent : MonoBehaviour
{
    public float HealthPoint { get; private set; }
    public float ManaPoint { get; private set; }
    public float JumpForce => _jumpForce + (float)_extraJumpForce.Value;

    public UnityEvent<float> OnHealthChanged { get; private set; } = new UnityEvent<float>();
    public UnityEvent<float> OnManaChanged { get; private set; } = new UnityEvent<float>();

    float _jumpForce;
    ExtraPoint _extraJumpForce;

    PlayerDataSO _data;

    public void Init(PlayerDataSO data)
    {
        _data = data;

        HealthPoint = _data.MaxHealthPoint;
        ManaPoint = _data.MaxManaPoint;

        _jumpForce = _data.JumpForce;
        _extraJumpForce = new ExtraPoint();
    }

    public void SetExtraJumpForce(float extra)
    {
        _extraJumpForce.Value = extra;
    }

    public bool UpdateHealth(float point)
    {
        var hp = Mathf.Clamp(HealthPoint + point, 0, _data.MaxHealthPoint);
        if (hp != HealthPoint)
        {
            HealthPoint = hp;
            OnHealthChanged?.Invoke(HealthPoint / _data.MaxHealthPoint);
            return true;
        }

        return false;
    }

    public bool UpdateMana(float point)
    {
        var mp = Mathf.Clamp(ManaPoint + point, 0, _data.MaxManaPoint);
        if (mp != ManaPoint)
        {
            ManaPoint = mp;
            OnManaChanged?.Invoke(ManaPoint / _data.MaxManaPoint);
            return true;
        }

        return false;
    }
}