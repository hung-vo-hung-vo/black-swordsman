using FishNet.Component.Animating;
using UnityEngine;

public class AnimationAgent : MonoBehaviour, IAttackable, IAnimatable
{
    [SerializeField] SkillAgent _skill;
    [SerializeField] Animator _anim;
    [SerializeField] AudioSource _moveJump, _attackTakeHit;

    PlayerDataSO _data;

    public void SetData(PlayerDataSO data)
    {
        _data = data;
    }

    public void PlayMoveSound()
    {
        PlaySound(_moveJump, _data.RunSound);
    }

    public void PlayJumpSound()
    {
        PlaySound(_moveJump, _data.JumpSound);
    }

    public void PlayAttackSound()
    {
        PlaySound(_attackTakeHit, _data.AttackSound);
    }

    public void PlayTakeHitSound()
    {
        PlaySound(_attackTakeHit, _data.TakeHitSound);
    }

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
        _anim.SetTrigger(param);
    }

    void PlaySound(AudioSource src, AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        src.PlayOneShot(clip);
    }
}