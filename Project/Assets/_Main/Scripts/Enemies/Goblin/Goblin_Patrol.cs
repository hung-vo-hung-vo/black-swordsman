using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Patrol : Patrol
{
    private Goblin enemy;

    public Goblin_Patrol(Entity entity, FiniteStateMachine stateMachine, string animBoolName, DataPatrol stateData, Goblin enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            // Debug.Log("Goblin_Patrol:LogicUpdate: isWall: " + isWall + ", isLedge: " + isLedge);
            enemy.idleState.SetFlip(true);
            FSM.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
