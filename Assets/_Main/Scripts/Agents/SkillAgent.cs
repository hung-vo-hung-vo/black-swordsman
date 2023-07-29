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

    private void Update()
    {
        if (_attackSemaphore)
        {
            var cldBounds = _damageCollier.bounds;
            var enemies = Physics2D.OverlapCircleAll(cldBounds.center, cldBounds.extents.x, LayerMask.GetMask(ApcsLayerMask.DAMAGEABLE_ENEMY));
            foreach (var e in enemies)
            {
                var atk = new AttackStats(new Vector2(cldBounds.center.x, cldBounds.center.y), Damage);
                e.gameObject.transform.parent.gameObject.SendMessage(Messages.ENEMY_RECEIVE_DAMAGE, atk);
            }
        }
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
        StartCoroutine(IEAttack(skill));
    }

    IEnumerator IEAttack(int skill)
    {
        if (!_skills.ContainsKey(skill) || _attackSemaphore)
        {
            yield break;
        }

        if (_stat.ManaPoint < _skills[skill].manaCost)
        {
            yield break;
        }

        _curSkill = _skills[skill];
        _stat.UpdateMana(-_curSkill.manaCost);
        _animator.SetTrigger(AnimationParam.Attack + skill.ToString());

        _attackSemaphore = _damageCollier.enabled = true;
        yield return new WaitForSeconds(_curSkill.delayTime);
        _attackSemaphore = _damageCollier.enabled = false;
    }
}