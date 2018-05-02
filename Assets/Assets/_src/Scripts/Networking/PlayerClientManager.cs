using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerClientManager : MonoBehaviour {

    public bool joinOnSceneLoad;
    private GameSettings settings;
    private NetworkClient client;
    private bool joined = false;

    public void Start()
    {
        client = NetworkManager.singleton?.client;
        if (Settings.gameSettings != null)
        {
            settings = Settings.Instance._settings;
        }
        else
        {
            settings = ScriptableObject.CreateInstance<GameSettings>();
        }
        if (joinOnSceneLoad)
        {
            JoinServer();
        }
    }

    public void Init(NetworkClient client)
    {
        this.client = client;

    }

    void Update()
    {
        //JoinServer();
    }

    public void JoinServer()
    {
        
        if (joined || client == null)
        {
            return;
        }
        joined = true;
        Debug.Log("Player joining server");
        Debug.Log(settings);
        PlayerJoinMessage message = new PlayerJoinMessage(
            1,
            settings.CharacterId
        );
        ClientScene.AddPlayer(client.connection, 0, message);
        //client.Send(PlayerJoinMessage.MESSAGE_TYPE, message);
    }
}
