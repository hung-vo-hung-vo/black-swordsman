using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_Dead : Dead
{
    private EvilWizard enemy;

    public EW_Dead(Entity entity, FiniteStateMachine FSM, string animationName, DeadData data, EvilWizard enemy) : base(entity, FSM, animationName, data)
    {
        this.enemy = enemy;
    }
}