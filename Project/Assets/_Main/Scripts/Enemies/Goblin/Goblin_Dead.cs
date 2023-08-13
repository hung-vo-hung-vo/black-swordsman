using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Dead : Dead
{
    private Goblin enemy;

    public Goblin_Dead(Entity entity, FiniteStateMachine FSM, string animationName, DeadData data, Goblin enemy) : base(entity, FSM, animationName, data)
    {
        this.enemy = enemy;
    }
}