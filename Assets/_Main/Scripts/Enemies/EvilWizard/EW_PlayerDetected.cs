using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_PlayerDetected : PlayerDetected
{
    private EvilWizard enemy;

    public EW_PlayerDetected(Entity entity, FiniteStateMachine FSM, string animationName, DataPlayerDeteced data, EvilWizard enemy) : base(entity, FSM, animationName, data)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.InitIcon(this.data.icon);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (canPerformCloseRangeSkill)
        {
            FSM.ChangeState(enemy.attackState);
        }
        else if (canPerformLongRangeSkill)
        {
            FSM.ChangeState(enemy.chaseState);
        }
        else if (!isPlayerInAgroRange)
        {
            FSM.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Check()
    {
        base.Check();
    }
}