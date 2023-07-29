using UnityEngine;

[System.Serializable]
public struct SpawnEnemy
{
    public ApcsNetworkBehaviour prefab;
    public Transform[] spawnPoints;
}