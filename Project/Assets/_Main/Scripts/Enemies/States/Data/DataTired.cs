using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTiredStateData", menuName = "Data/State Data/Tired State")]
public class TiredData : ScriptableObject
{
    public float tiredTime = 2f;
    public GameObject icon = null;
}