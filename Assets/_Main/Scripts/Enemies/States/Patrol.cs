using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    private DataPatrol data;

    protected bool isWall;
    protected bool isLedge;

    public Patrol(Entity entity, FiniteStateMachine FSM, string animationName, DataPatrol data) : base(entity, FSM, animationName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(data.movementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isWall = entity.CheckWall();
        isLedge = entity.CheckLedge();

        if (isWall || !isLedge)
        {
            entity.Flip();
            entity.SetVelocityX(data.movementSpeed);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
