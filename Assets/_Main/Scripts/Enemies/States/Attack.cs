using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    protected Transform attackPosition;

    protected bool isPlayerInMinAgroRange;
    protected bool finished;
    public bool isPerformingAttack;

    public Attack(Entity entity, FiniteStateMachine FSM, string animationName, Transform attackPosition) : base(entity, FSM, animationName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Check()
    {
        base.Check();

        isPlayerInMinAgroRange = entity.CheckPlayerInAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.a2s.attackState = this;
        entity.SetVelocityX(0f);
        entity.SetVelocityY(0f);
        finished = false;
        // isPerformingAttack = true;
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

    public virtual void TriggerAttack()
    {
<<<<<<< HEAD
        isPerformingAttack = true;
=======

>>>>>>> main
    }

    public virtual void FinishAttack()
    {
        finished = true;
        isPerformingAttack = false;
    }
}