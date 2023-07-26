using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_Chase : Chase
{
    private EvilWizard enemy;

    public EW_Chase(Entity entity, FiniteStateMachine FSM, string animationName, ChaseData data, EvilWizard enemy) : base(entity, FSM, animationName, data)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isLedge || isWall)
        {
            // Debug.Log("EW_Chase:LogicUpdate: isLedge: " + isLedge + ", isWall: " + isWall);
            // enemy.SetVelocityX(0f);
            FSM.ChangeState(enemy.lookForPlayerState);
        }
        else if (isChaseTimeOver)
        {
            FSM.ChangeState(enemy.tiredState);
        }
    }
}