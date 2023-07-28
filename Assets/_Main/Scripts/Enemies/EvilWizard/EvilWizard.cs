using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : Entity
{
    public EW_Idle idleState { get; private set; }
    public EW_Patrol patrolState { get; private set; }

    [SerializeField] private DataIdle idleData;
    [SerializeField] private DataPatrol patrolData;

    public override void Start()
    {
        base.Start();

        idleState = new EW_Idle(this, FSM, "idle", idleData, this);
        patrolState = new EW_Patrol(this, FSM, "patrol", patrolData, this);

        FSM.Initialize(patrolState);
    }
}
