using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAgent : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Collider2D _damageCollier;

    bool _attackSemaphore = false;
    Dictionary<int, SkillData> _skills;
    SkillData _curSkill;
    ExtraPoint _extraDamage;
    StatAgent _stat;

    public float Damage => _curSkill.damage + (float)_extraDamage.Value;
    Vector3 _dmgCldBoundsCenter => _damageCollier.bounds.center;
    AttackStats _atk => new AttackStats(_dmgCldBoundsCenter, Damage);

    private void Awake()
    {
        SetAttackStatus(false);
    }

    public void Init(StatAgent stat, Dictionary<int, SkillData> skills)
    {
        _stat = stat;
        _skills = skills;
        _extraDamage = new ExtraPoint();
    }

    public void SetExtraDamage(float extra)
    {
        _extraDamage.Value = extra;
    }

    public void Attack(int skill)
    {
        if (!_skills.ContainsKey(skill) || _attackSemaphore)
        {
            return;
        }

        if (_stat.ManaPoint < _skills[skill].manaCost)
        {
            return;
        }

        _curSkill = _skills[skill];
        _stat.UpdateMana(-_curSkill.manaCost);
        _animator.SetTrigger(AnimationParam.Attack + skill.ToString());
    }

    public void SetAttackStatus(bool status)
    {
        _attackSemaphore = _damageCollier.enabled = status;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ApcsLayerMask.DAMAGEABLE_ENEMY))
        {
            other.gameObject.transform.parent.gameObject.SendMessage(Messages.ENEMY_RECEIVE_DAMAGE, _atk);
            SetAttackStatus(false);
        }
    }
}