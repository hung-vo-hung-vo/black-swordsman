using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Entity
{
    public Skeleton_Idle idleState { get; private set; }
    public Skeleton_Patrol patrolState { get; private set; }
    public Skeleton_PlayerDetected playerDetectedState { get; private set; }
    public Skeleton_Chase chaseState { get; private set; }
    public Skeleton_LookingForPlayer lookForPlayerState { get; private set; }
    public Skeleton_Tired tiredState { get; private set; }
    public Skeleton_Dead deadState { get; private set; }
    public Skeleton_MeleeAttack attackState { get; private set; }
    public Skeleton_TakeHit takeHitState { get; private set; }
    // public Skeleton_MeleeAttack attack2State { get; private set; }

    [SerializeField] private DataIdle idleData;
    [SerializeField] private DataPatrol patrolData;
    [SerializeField] private DataPlayerDeteced playerDetectedData;
    [SerializeField] private ChaseData chaseData;
    [SerializeField] private LookingForPlayerData lookingForPlayerData;
    [SerializeField] private TiredData tiredData;
    [SerializeField] private DeadData deadData;
    [SerializeField] private MeleeAttackData attackData;
    // [SerializeField] private MeleeAttackData attack2Data;
    [SerializeField] private TakeHitData takeHitData;
    [SerializeField] private Transform attackPosition;

    public override void Start()
    {
        base.Start();

        idleState = new Skeleton_Idle(this, FSM, "idle", idleData, this);
        patrolState = new Skeleton_Patrol(this, FSM, "patrol", patrolData, this);
        playerDetectedState = new Skeleton_PlayerDetected(this, FSM, "playerDetected", playerDetectedData, this);
        chaseState = new Skeleton_Chase(this, FSM, "chase", chaseData, this);
        lookForPlayerState = new Skeleton_LookingForPlayer(this, FSM, "lookingForPlayer", lookingForPlayerData, this);
        tiredState = new Skeleton_Tired(this, FSM, "tired", tiredData, this);
        deadState = new Skeleton_Dead(this, FSM, "dead", deadData, this);
        attackState = new Skeleton_MeleeAttack(this, FSM, "attack1", attackPosition, attackData, this);
        // attack2State = new Skeleton_MeleeAttack(this, FSM, "attack2", attackPosition, attack2Data, this);
        takeHitState = new Skeleton_TakeHit(this, FSM, "takeHit", takeHitData, this);

        FSM.Initialize(patrolState);
    }

    public override void ReceiveDamage(AttackStats attackStats)
    {
        if (isDead)
        {
            return;
        }

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