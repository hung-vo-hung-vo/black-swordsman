using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Dead : Dead
{
    private Skeleton enemy;

    public Skeleton_Dead(Entity entity, FiniteStateMachine FSM, string animationName, DeadData data, Skeleton enemy) : base(entity, FSM, animationName, data)
    {
        this.enemy = enemy;
    }
}