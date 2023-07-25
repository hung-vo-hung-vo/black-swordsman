using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookingForPlayerData", menuName = "Data/State Data/Looking For Player Data")]
public class LookingForPlayerData : ScriptableObject
{
    public int amountOfTurns = 2;
    public float cdTurnTime = 0.75f;
}