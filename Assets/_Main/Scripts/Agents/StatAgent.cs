using UnityEngine;

public class StatAgent : MonoBehaviour
{
    public float HealthPoint { get; private set; }
    public float ManaPoint { get; private set; }
    public float JumpForce => _jumpForce + (float)_extraJumpForce.Value;

    float _jumpForce;
    ExtraPoint _extraJumpForce;

    PlayerDataSO _data;

    public void Init(PlayerDataSO data)
    {
        HealthPoint = data.MaxHealthPoint;
        ManaPoint = data.MaxManaPoint;

        _jumpForce = data.JumpForce;
        _extraJumpForce = new ExtraPoint();

        _data = data;
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