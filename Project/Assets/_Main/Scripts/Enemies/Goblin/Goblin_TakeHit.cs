using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_TakeHit : TakeHit
{
    private Goblin enemy;

    public Goblin_TakeHit(Entity entity, FiniteStateMachine FSM, string animationName, TakeHitData data, Goblin enemy) : base(entity, FSM, animationName, data)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        // TODO: Add icon
        // enemy.InitIcon(data.icon);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isGround && Time.time >= startTime + data.delayTime)
        {
            entity.SetVelocityY(0f);
            if (isPlayerInAgroRange)
            {
                FSM.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                FSM.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}