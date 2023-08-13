using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tired : State
{
    protected TiredData data;

    protected bool isPlayerInAgroRange;
    protected bool isTiredTimeOver;

    public Tired(Entity entity, FiniteStateMachine FSM, string animationName, TiredData data) : base(entity, FSM, animationName)
    {
        this.data = data;
    }

    public override void Check()
    {
        base.Check();

        isPlayerInAgroRange = entity.CheckPlayerInAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isTiredTimeOver = false;

        entity.SetVelocityX(0f);
        entity.InitIcon(data.icon);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + data.tiredTime)
        {
            isTiredTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}