using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_Idle : Idle
{
    private EvilWizard enemy;
    public EW_Idle(Entity entity, FiniteStateMachine stateMachine, string animBoolName, DataIdle stateData, EvilWizard enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isIdleTimeOver)
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
