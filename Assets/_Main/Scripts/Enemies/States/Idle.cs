using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    protected DataIdle data;
    protected bool flip;
    protected bool isIdleTimeOver;

    protected float idleTime;

    public Idle(Entity entity, FiniteStateMachine FSM, string animationName, DataIdle data) : base(entity, FSM, animationName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }


    public override void Exit()
    {
        base.Exit();

        if (flip)
        {
            entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
            flip = false;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlip(bool flip)
    {
        this.flip = flip;
    }
    private void SetRandomIdleTime()
    {
        idleTime = UnityEngine.Random.Range(data.minTime, data.maxTime);
    }
}
