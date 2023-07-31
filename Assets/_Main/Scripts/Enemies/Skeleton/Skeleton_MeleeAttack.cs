using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_MeleeAttack : MeleeAttack
{
    private Skeleton enemy;

    public Skeleton_MeleeAttack(Entity entity, FiniteStateMachine FSM, string animationName, Transform attackPosition, MeleeAttackData attackData, Skeleton enemy) : base(entity, FSM, animationName, attackPosition, attackData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (finished)
        {
            if (!isGround)
            {
                FSM.ChangeState(enemy.takeHitState);
            }
            else if (isPlayerInMinAgroRange)
            {
                FSM.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                FSM.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}