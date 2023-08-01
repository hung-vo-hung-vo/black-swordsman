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

        // isFinishedDead = false;

        entity.SetVelocityX(0f);
        // entity.InitIcon(data.icon);

        entity.Dropper.SetDropPoint(entity.body.transform);
        entity.Dropper.DropItem();
        entity.a2s.deadState = this;
        // Debug.Log("WTF man!");
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
        // isFinishedDead = true;
        // Debug.Log("????");
        entity.gameObject.SetActive(false);
    }
}