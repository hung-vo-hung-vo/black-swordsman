using System;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;

public class StartMenuScener : MonoBehaviour
{
    public void StartStoryGame()
    {
        GameManager.Instance.NetworkManager.ClientManager.OnClientConnectionState += OnStoryClientConnState;
        GameManager.Instance.ConnectToServer(false);
    }

    private void OnStoryClientConnState(ClientConnectionStateArgs args)
    {
        GameManager.Instance.NetworkManager.ClientManager.OnClientConnectionState -= OnStoryClientConnState;
        switch (args.ConnectionState)
        {
            case LocalConnectionState.Started:
                ApcsSceneLoader.Instance.LoadStoryGame();
                break;
            case LocalConnectionState.Stopping:
                break;
        }
    }

    public void StartCoopGame()
    {
        GameManager.Instance.ConnectToServer(true);
    }
}