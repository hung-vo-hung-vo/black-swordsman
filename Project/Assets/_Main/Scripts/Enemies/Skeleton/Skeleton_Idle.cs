using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Idle : Idle
{
    private Skeleton enemy;
    public Skeleton_Idle(Entity entity, FiniteStateMachine stateMachine, string animBoolName, DataIdle stateData, Skeleton enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            // isIdleTimeOver = true;
            FSM.ChangeState(enemy.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            // Debug.Log("Idle time over");
            FSM.ChangeState(enemy.patrolState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
