using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim2State : MonoBehaviour
{
    public Attack attackState;
    public Dead deadState;
    // public TakeHit takeHitState;

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