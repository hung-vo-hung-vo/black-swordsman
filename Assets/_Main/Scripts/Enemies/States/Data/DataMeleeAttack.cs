using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Attack Data", menuName = "Data/State Data/Melee Attack Data")]
public class MeleeAttackData : ScriptableObject
{
    public float radius = 0.5f;
    public float damage = 10f;

    // public LayerMask playerLayer;
}