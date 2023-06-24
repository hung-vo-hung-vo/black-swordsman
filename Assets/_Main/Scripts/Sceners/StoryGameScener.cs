using System.Collections;
using System.Threading.Tasks;
using FishNet;
using FishNet.Object;
using UnityEngine;

public class StoryGameScener : NetworkBehaviour
{
    GameObject _player;

    public override async void OnStartClient()
    {
        base.OnStartClient();

        var waitTime = 3f;
        const float checkTime = 0.5f;
        while (!base.IsOwner && waitTime > 0f)
        {
            waitTime -= checkTime;
            await Task.Delay((int)(checkTime * 1000));
        }

        if (base.IsOwner)
        {
            _player = Instantiate(StoryGameManager.Instance.PlayerPrefab, transform.position, Quaternion.identity);
            ServerManager.Spawn(_player, ClientManager.Connection);
        }
        else
        {
            Debug.LogError(base.IsOwner);
        }
    }
}