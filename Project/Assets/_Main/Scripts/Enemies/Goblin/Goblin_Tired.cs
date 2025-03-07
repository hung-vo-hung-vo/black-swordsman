using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Tired : Tired
{
    private Goblin enemy;

    public Goblin_Tired(Entity entity, FiniteStateMachine FSM, string animationName, TiredData data, Goblin enemy) : base(entity, FSM, animationName, data)
    {
        this.enemy = enemy;
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

        if (isTiredTimeOver)
        {
            if (isPlayerInAgroRange)
            {
                FSM.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                FSM.ChangeState(enemy.lookForPlayerState);
            }
        }

        // TODO: Add check getting hit
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Check()
    {
        base.Check();
    }
}