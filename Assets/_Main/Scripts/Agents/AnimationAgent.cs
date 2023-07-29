using UnityEngine;

public class AnimationAgent : MonoBehaviour, IAttackable
{
    [SerializeField] SkillAgent _skill;

    public void DisableAttack()
    {
        _skill.SetAttackStatus(false);
    }

    public void EnableAttack()
    {
        _skill.SetAttackStatus(true);
    }
}