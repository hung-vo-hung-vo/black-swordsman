using System.Collections;
using System.Threading.Tasks;
using FishNet;
using FishNet.Object;
using UnityEngine;

public class StoryGameScener : MonoBehaviour
{
    GameObject _player;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _player = Instantiate(StoryGameManager.Instance.PlayerPrefab, transform.position, Quaternion.identity);
    }
}