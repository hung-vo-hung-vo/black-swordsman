using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    protected MeleeAttackData data;
    protected AttackStats stats;

    protected bool isGround;

    public MeleeAttack(Entity entity, FiniteStateMachine FSM, string animationName, Transform attackPosition, MeleeAttackData data) : base(entity, FSM, animationName, attackPosition)
    {
        this.data = data;
    }

    public override void Check()
    {
        base.Check();

        isGround = entity.CheckGround();
    }

    public override void Enter()
    {
        base.Enter();

        stats.position = attackPosition.position;
        stats.damage = data.damage * (GameManager.Instance.HardMode ? 3f : 1f);
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPosition.position, data.radius, entity.data.playerLayer);

        foreach (Collider2D player in hit)
        {
            player.transform.SendMessage(Messages.PLAYER_TAKE_DAMAGE, stats);
        }
    }
}