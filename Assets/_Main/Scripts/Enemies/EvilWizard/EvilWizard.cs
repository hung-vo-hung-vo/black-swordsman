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

    [SerializeField] private DataIdle idleData;
    [SerializeField] private DataPatrol patrolData;
    [SerializeField] private DataPlayerDeteced playerDetectedData;
    [SerializeField] private ChaseData chaseData;
    [SerializeField] private LookingForPlayerData lookingForPlayerData;
    [SerializeField] private TiredData tiredData;

    public override void Start()
    {
        base.Start();

        idleState = new EW_Idle(this, FSM, "idle", idleData, this);
        patrolState = new EW_Patrol(this, FSM, "patrol", patrolData, this);
        playerDetectedState = new EW_PlayerDetected(this, FSM, "playerDetected", playerDetectedData, this);
        chaseState = new EW_Chase(this, FSM, "chase", chaseData, this);
        lookForPlayerState = new EW_LookingForPlayer(this, FSM, "lookingForPlayer", lookingForPlayerData, this);
        tiredState = new EW_Tired(this, FSM, "tired", tiredData, this);

        FSM.Initialize(patrolState);
    }
}
