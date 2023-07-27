using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : State
{
    private DeadData data;
    public Dead(Entity entity, FiniteStateMachine FSM, string animationName, DeadData data) : base(entity, FSM, animationName)
    {
        this.data = data;
    }

    public override void Check()
    {
        base.Check();
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(0f);
        // entity.InitIcon(data.icon);

        entity.a2s.deadState = this;
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

    public virtual void FinishDead()
    {
        entity.gameObject.transform.parent.gameObject.SetActive(false);
    }
}