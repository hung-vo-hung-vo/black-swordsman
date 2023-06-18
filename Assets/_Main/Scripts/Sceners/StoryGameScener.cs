using FishNet;
using UnityEngine;

public class StoryGameScener : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;

    Player _player;

    private void Start()
    {
        _player = Instantiate(StoryGameManager.Instance.PlayerPrefab, _spawnPoint.position, Quaternion.identity);
        InstanceFinder.ServerManager.Spawn(_player.gameObject);
    }
}