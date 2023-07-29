using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : Entity
{
    public EW_Idle idleState { get; private set; }
    public EW_Patrol patrolState { get; private set; }
    public EW_PlayerDetected playerDetectedState { get; private set; }
    public EW_Chase chaseState { get; private set; }
    public EW_LookingForPlayer lookForPlayerState { get; private set; }
    public EW_Tired tiredState { get; private set; }
    public EW_Dead deadState { get; private set; }
    public EW_MeleeAttack attackState { get; private set; }
    public EW_TakeHit takeHitState { get; private set; }

    [SerializeField] private DataIdle idleData;
    [SerializeField] private DataPatrol patrolData;
    [SerializeField] private DataPlayerDeteced playerDetectedData;
    [SerializeField] private ChaseData chaseData;
    [SerializeField] private LookingForPlayerData lookingForPlayerData;
    [SerializeField] private TiredData tiredData;
    [SerializeField] private DeadData deadData;
    [SerializeField] private MeleeAttackData attackData;
    [SerializeField] private TakeHitData takeHitData;
    [SerializeField] private Transform attackPosition;

    public override void Start()
    {
        base.Start();

        idleState = new EW_Idle(this, FSM, "idle", idleData, this);
        patrolState = new EW_Patrol(this, FSM, "patrol", patrolData, this);
        playerDetectedState = new EW_PlayerDetected(this, FSM, "playerDetected", playerDetectedData, this);
        chaseState = new EW_Chase(this, FSM, "chase", chaseData, this);
        lookForPlayerState = new EW_LookingForPlayer(this, FSM, "lookingForPlayer", lookingForPlayerData, this);
        tiredState = new EW_Tired(this, FSM, "tired", tiredData, this);
        deadState = new EW_Dead(this, FSM, "dead", deadData, this);
        attackState = new EW_MeleeAttack(this, FSM, "attack", attackPosition, attackData, this);
        takeHitState = new EW_TakeHit(this, FSM, "takeHit", takeHitData, this);

        FSM.Initialize(patrolState);
    }

    public override void ReceiveDamage(AttackStats attackStats)
    {
        base.ReceiveDamage(attackStats);

        if (CheckGround()) // && !attackState.isPerformingAttack
        {
            // Debug.Log("fly");
            Knockback(takeHitData.knockbackSpeed, takeHitData.angle, lastDamageDirection);
        }

        if (isDead)
        {
            FSM.ChangeState(deadState);
        }
        else if (!attackState.isPerformingAttack)
        {
            // Debug.Log("take hit");
            FSM.ChangeState(takeHitState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackData.radius);
        Gizmos.color = Color.white;
#endif
    }
}