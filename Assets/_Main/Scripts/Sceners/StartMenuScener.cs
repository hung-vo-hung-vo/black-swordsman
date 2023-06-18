using System;
using FishNet;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;

public class StartMenuScener : MonoBehaviour
{
    public void StartStoryGame()
    {
        InstanceFinder.ClientManager.OnClientConnectionState += OnStoryClientConnState;
        GameManager.Instance.ConnectToServer(false);
    }

    void OnStoryClientConnState(ClientConnectionStateArgs args)
    {
        switch (args.ConnectionState)
        {
            case LocalConnectionState.Started:
                InstanceFinder.ClientManager.OnClientConnectionState -= OnStoryClientConnState;
                ApcsSceneLoader.Instance.LoadStoryGame();
                return;
            case LocalConnectionState.Stopping:
                InstanceFinder.ClientManager.OnClientConnectionState -= OnStoryClientConnState;
                break;
        }
    }

    public void StartCoopGame()
    {
        GameManager.Instance.ConnectToServer(true);
    }
}