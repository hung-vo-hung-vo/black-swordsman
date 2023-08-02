using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim2State : MonoBehaviour
{
    public Attack attackState;
    public Dead deadState;
    // public TakeHit takeHitState;

    [SerializeField] AudioClip _attackSound;
    [SerializeField] AudioSource _audioSource;

    public void PlayAttackSound()
    {
        _audioSource.PlayOneShot(_attackSound);
    }

    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }

    private void FinishDead()
    {
        deadState.FinishDead();
    }


    // private void FinishTakeHit()
    // {
    //     takeHitState.FinishTakeHit();
    // }
}