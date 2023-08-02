using System;
using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] string _serverIP;
    [SerializeField] ushort _serverPort;
    [field: SerializeField] public AudioSource Audio { get; private set; }

    public bool HardMode { get; private set; }
    public void SetDifficulty(bool hard)
    {
        HardMode = hard;
    }

    public NetworkManager NetworkManager { get; private set; }

    public static bool IsDebug()
    {
        var isDebug = false;
#if UNITY_EDITOR
        isDebug = true;
#endif
        return isDebug;
    }

    protected override void Awake()
    {
        base.Awake();
        // NetworkManager = FindObjectOfType<NetworkManager>();
        // if (Application.platform == RuntimePlatform.LinuxServer)
        // {
        //     NetworkManager.ServerManager.StartConnection(_serverPort);
        // }
    }

    public void ConnectToServer(bool isCoopMode)
    {
        if (isCoopMode)
        {
            NetworkManager.ClientManager.StartConnection(_serverIP, _serverPort);
            return;
        }

        NetworkManager.ServerManager.StartConnection();
        NetworkManager.ClientManager.StartConnection();
    }
}