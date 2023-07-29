using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetected : State
{
    protected DataPlayerDeteced data;

    protected bool isPlayerInAgroRange;
    protected bool canPerformLongRangeSkill;
    protected bool canPerformCloseRangeSkill;
    protected bool isLedge;

    public PlayerDetected(Entity entity, FiniteStateMachine FSM, string animationName, DataPlayerDeteced data) : base(entity, FSM, animationName)
    {
        this.data = data;
    }

    public override void Check()
    {
        // base.Check();

        isPlayerInAgroRange = entity.CheckPlayerInAgroRange();
        isLedge = entity.CheckLedge();
        canPerformCloseRangeSkill = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        canPerformLongRangeSkill = true;
        entity.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + data.cdLongRangeSkill)
        {
            canPerformLongRangeSkill = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}