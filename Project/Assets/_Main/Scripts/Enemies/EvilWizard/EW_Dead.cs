using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_Dead : Dead
{
    private EvilWizard enemy;

    public EW_Dead(Entity entity, FiniteStateMachine FSM, string animationName, DeadData data, EvilWizard enemy) : base(entity, FSM, animationName, data)
    {
        this.enemy = enemy;
    }

    public override void Check()
    {
        base.Check();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishDead()
    {
        base.FinishDead();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}