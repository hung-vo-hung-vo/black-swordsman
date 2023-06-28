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

    public float Damage => _curSkill.damage;

    public void SetSkills(Dictionary<int, SkillData> skills)
    {
        _skills = skills;
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

        _curSkill = _skills[skill];
        _animator.SetTrigger(AnimationParam.Attack + skill.ToString());

        _attackSemaphore = _damageCollier.enabled = true;
        yield return new WaitForSeconds(_curSkill.delayTime);
        _attackSemaphore = _damageCollier.enabled = false;
    }
}