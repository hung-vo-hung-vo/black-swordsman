using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_Patrol : Patrol
{
    private EvilWizard enemy;

    public EW_Patrol(Entity entity, FiniteStateMachine stateMachine, string animBoolName, DataPatrol stateData, EvilWizard enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isPlayerInMinAgroRange)
        {
            // enemy.idleState.SetFlip(false);
            FSM.ChangeState(enemy.playerDetectedState);
        }
        else if (isWall || !isLedge)
        {
            // Debug.Log(isWall + " " + isLedge);
            enemy.idleState.SetFlip(true);
            FSM.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
