using FishNet;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] SpawnEnemy[] _enemies;

    private void Start()
    {
        foreach (var pE in _enemies)
        {
            if (pE.prefab == null)
            {
                continue;
            }

            foreach (var p in pE.spawnPoints)
            {
                if (p != null)
                {
                    var e = Instantiate(pE.prefab.gameObject, p.position, p.rotation);
                    InstanceFinder.ServerManager.Spawn(e);
                }
            }
        }
    }
}