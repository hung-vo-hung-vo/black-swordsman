using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    private DataPatrol data;

    protected bool isWall;
    protected bool isLedge;
    protected bool isPlayerInMinAgroRange;

    public Patrol(Entity entity, FiniteStateMachine FSM, string animationName, DataPatrol data) : base(entity, FSM, animationName)
    {
        this.data = data;
    }

    public override void Check()
    {
        // base.Check();
        isWall = entity.CheckWall();
        isLedge = entity.CheckLedge();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

        // if (isWall || !isLedge) Debug.Log("Patrol:Check: isWall: " + isWall + ", isLedge: " + isLedge);
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

        if (isWall || !isLedge)
        {
            // Debug.Log("Patrol:LogicUpdate: isWall: " + isWall + ", isLedge: " + isLedge);
            entity.Flip();
            entity.SetVelocityX(data.movementSpeed);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
