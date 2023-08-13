using UnityEngine;
using UnityEngine.Events;

public class StatAgent : MonoBehaviour, IHudable
{
    public float JumpForce => _jumpForce + (float)_extraJumpForce.Value;
    public float HealthPoint
    {
        get => _healthPoint;
        private set
        {
            _healthPoint = value;
            OnHealthChanged()?.Invoke(_healthPoint / _data.MaxHealthPoint);
        }
    }
    public float ManaPoint
    {
        get => _manaPoint;
        private set
        {
            _manaPoint = value;
            OnManaChanged()?.Invoke(_manaPoint / _data.MaxManaPoint);
        }
    }

    UnityEvent<float> _onHealthChanged = new UnityEvent<float>();
    UnityEvent<float> _onManaChanged = new UnityEvent<float>();

    public UnityEvent<float> OnHealthChanged() => _onHealthChanged;
    public UnityEvent<float> OnManaChanged() => _onManaChanged;

    float _jumpForce;
    ExtraPoint _extraJumpForce;

    float _healthPoint;

    float _manaPoint;
    float _manaAutoHealDelay = 0f;

    PlayerDataSO _data;

    private void Update()
    {
        if (_data == null)
        {
            return;
        }

        _manaAutoHealDelay += Time.deltaTime;
        if (_manaAutoHealDelay >= _data.ManaAutoHealDelay)
        {
            UpdateMana(_data.ManaAutoHeal);
            _manaAutoHealDelay = 0f;
        }
    }

    public void Init(PlayerDataSO data)
    {
        _data = data;
        FindObjectOfType<Hud>()?.Init(this);

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
            return true;
        }

        return false;
    }
}