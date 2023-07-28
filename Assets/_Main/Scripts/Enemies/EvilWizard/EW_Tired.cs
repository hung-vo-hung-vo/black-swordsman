using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_Tired : Tired
{
    private EvilWizard enemy;

    public EW_Tired(Entity entity, FiniteStateMachine FSM, string animationName, TiredData data, EvilWizard enemy) : base(entity, FSM, animationName, data)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isTiredTimeOver)
        {
            if (isPlayerInAgroRange)
            {
                FSM.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                FSM.ChangeState(enemy.lookForPlayerState);
            }
        }

        // TODO: Add check getting hit
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Check()
    {
        base.Check();
    }
}