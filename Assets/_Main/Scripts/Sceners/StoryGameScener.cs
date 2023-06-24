using System.Collections;
using System.Threading.Tasks;
using FishNet;
using FishNet.Object;
using UnityEngine;

public class StoryGameScener : ApcsNetworkBehaviour
{
    GameObject _player;

    public override void OnStartClient()
    {
        base.OnStartClient();
        IfIsOwnerThenDo(SpawnPlayer);
    }

    private void SpawnPlayer()
    {
        _player = Instantiate(StoryGameManager.Instance.PlayerPrefab, transform.position, Quaternion.identity);
        ServerManager.Spawn(_player, ClientManager.Connection);
    }
}