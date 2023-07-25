using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetected : State
{
    private DataPlayerDeteced data;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    public PlayerDetected(Entity entity, FiniteStateMachine FSM, string animationName, DataPlayerDeteced data) : base(entity, FSM, animationName)
    {
        this.data = data;
    }

    public override void Check()
    {
        base.Check();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}