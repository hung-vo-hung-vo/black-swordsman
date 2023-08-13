using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackStats
{
    public Vector2 position;
    public float damage;

    public AttackStats(Vector2 position, float damage)
    {
        this.position = position;
        this.damage = damage;
    }
}