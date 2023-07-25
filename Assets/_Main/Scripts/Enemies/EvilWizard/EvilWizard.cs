using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : Entity
{
    public EW_Idle idleState { get; private set; }
    public EW_Patrol patrolState { get; private set; }
    public EW_PlayerDetected playerDetectedState { get; private set; }

    [SerializeField] private DataIdle idleData;
    [SerializeField] private DataPatrol patrolData;
    [SerializeField] private DataPlayerDeteced playerDetectedData;
    [SerializeField] private GameObject noticeSign;

    public override void Start()
    {
        base.Start();

        idleState = new EW_Idle(this, FSM, "idle", idleData, this);
        patrolState = new EW_Patrol(this, FSM, "patrol", patrolData, this);
        playerDetectedState = new EW_PlayerDetected(this, FSM, "playerDetected", playerDetectedData, this);

        FSM.Initialize(patrolState);
    }

    public void InitSign()
    {
        Instantiate(noticeSign,
                    avatar.transform.position,
                    Quaternion.Euler(0f, 0f, 0f)
                   );
    }
}
