using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State")]
public class DataPlayerDeteced : ScriptableObject
{
    public float cdLongRangeSkill = 2f;
    public GameObject icon = null;
}