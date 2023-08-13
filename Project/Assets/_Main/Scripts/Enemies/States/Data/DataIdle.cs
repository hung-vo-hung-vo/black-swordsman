using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDataForIdle", menuName = "Data/State Data/Idle Data")]
public class DataIdle : ScriptableObject
{
    public float minTime = 1f;
    public float maxTime = 2f;
}
