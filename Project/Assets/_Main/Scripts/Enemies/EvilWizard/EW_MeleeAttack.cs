using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_MeleeAttack : MeleeAttack
{
    private EvilWizard enemy;

    public EW_MeleeAttack(Entity entity, FiniteStateMachine FSM, string animationName, Transform attackPosition, MeleeAttackData attackData, EvilWizard enemy) : base(entity, FSM, animationName, attackPosition, attackData)
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