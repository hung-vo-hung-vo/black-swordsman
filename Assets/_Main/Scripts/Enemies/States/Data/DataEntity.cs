using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class DataEntity : ScriptableObject
{
    public float maxHealth = 50f;
    // public float damageHopSpeed = 3f;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckLength = 0.3f;

    public float agroRange = 3f;
    public float closeRangeActionDistance = 1f;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
}