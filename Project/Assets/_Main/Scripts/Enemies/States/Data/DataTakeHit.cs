using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Take Hit Data", menuName = "Data/State Data/Take Hit Data")]
public class TakeHitData : ScriptableObject
{
    public float knockbackSpeed = 3f;
    public Vector2 angle = new Vector2(5, 10);
    public float delayTime = 0.2f;
}