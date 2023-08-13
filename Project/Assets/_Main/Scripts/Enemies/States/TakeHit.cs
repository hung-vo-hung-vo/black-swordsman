using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHit : State
{
    public TakeHitData data { get; private set; }

    protected bool isPlayerInAgroRange;
    protected bool isGround;

    public TakeHit(Entity entity, FiniteStateMachine FSM, string animationName, TakeHitData data) : base(entity, FSM, animationName)
    {
        this.data = data;
    }

    public override void Check()
    {
        base.Check();

        isPlayerInAgroRange = entity.CheckPlayerInAgroRange();
        isGround = entity.CheckGround();
    }

    public override void Enter()
    {
        base.Enter();

        // finishAnim = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // if (isGround)
        // {
        //     entity.SetVelocityX(0f);
        //     entity.SetVelocityY(0f);
        // }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // public void FinishTakeHit()
    // {
    //     // finishAnim = true;
    // }
}