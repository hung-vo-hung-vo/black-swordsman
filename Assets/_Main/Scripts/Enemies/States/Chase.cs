using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    protected ChaseData data;

    protected bool isPlayerInMinAgroRange;
    protected bool isLedge;
    protected bool isWall;
    protected bool isChaseTimeOver;

    public Chase(Entity entity, FiniteStateMachine FSM, string animationName, ChaseData data) : base(entity, FSM, animationName)
    {
        this.data = data;
    }

    public override void Check()
    {
        // base.Check();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isLedge = entity.CheckLedge();
        isWall = entity.CheckWall();
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(data.chaseSpeed);
        isChaseTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + data.chaseTime)
        {
            isChaseTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
