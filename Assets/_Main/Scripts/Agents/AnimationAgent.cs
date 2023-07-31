using FishNet.Component.Animating;
using UnityEngine;

public class AnimationAgent : MonoBehaviour, IAttackable, IAnimatable
{
    [SerializeField] SkillAgent _skill;
    [SerializeField] NetworkAnimator _netAnim;
    [SerializeField] Animator _anim;

    public void DisableAttack()
    {
        _skill.SetAttackStatus(false);
    }

    public void EnableAttack()
    {
        _skill.SetAttackStatus(true);
    }

    public void SetBool(string param, bool value)
    {
        _anim.SetBool(param, value);
    }

    public void SetTrigger(string param)
    {
        _netAnim.SetTrigger(param);
    }
}