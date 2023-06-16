using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player", order = 0)]
public class PlayerDataSO : ScriptableObject
{
    [field: SerializeField] public float MaxHealthPoint { get; private set; }

    [field: SerializeField] public float RunSpeed { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
}