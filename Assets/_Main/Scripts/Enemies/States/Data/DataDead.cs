using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadData", menuName = "Data/State Data/Dead Data")]
public class DeadData : ScriptableObject
{
    public GameObject deathChunkParticle;
    public GameObject icon;
}