using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingForPlayer : State
{
    protected LookingForPlayerData data;

    protected bool canTurn;
    protected bool isPlayerInMinAgroRange;
    protected bool doneAllTurns;

    protected float lastTurnTime;
    protected int turnsCnt;

    public LookingForPlayer(Entity entity, FiniteStateMachine FSM, string animationName, LookingForPlayerData data) : base(entity, FSM, animationName)
    {
        this.data = data;
    }

    public override void Check()
    {
        // base.Check();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        doneAllTurns = false;
        lastTurnTime = startTime;
        turnsCnt = 0;

        canTurn = false;

        entity.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (canTurn)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            turnsCnt++;
            canTurn = false;
        }
        else if (Time.time >= lastTurnTime + data.cdTurnTime && turnsCnt < data.amountOfTurns)
        {
            canTurn = true;
        }
        else if (turnsCnt >= data.amountOfTurns && Time.time >= lastTurnTime + data.cdTurnTime)
        {
            doneAllTurns = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
