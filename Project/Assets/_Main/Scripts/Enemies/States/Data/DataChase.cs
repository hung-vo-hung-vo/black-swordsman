using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChaseData", menuName = "Data/State Data/Chase Data")]
public class ChaseData : ScriptableObject
{
    public float chaseSpeed = 10f;
    public float chaseTime = 2f;
}