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
        if(joinOnSceneLoad) {
            //JoinServer();
        }
        if (Settings.gameSettings != null)
        {
            settings = Settings.gameSettings;
        }
        else
        {
            settings = ScriptableObject.CreateInstance<GameSettings>();
        }
    }

    public void Init(NetworkClient client)
    {
        this.client = client;

    }

    void Update()
    {
        JoinServer();    
    }

    public void JoinServer()
    {
        if (joined || client == null)
        {
            return;
        }
        Debug.Log("Player joining server");
        PlayerJoinMessage message = new PlayerJoinMessage(
            client.connection.connectionId,
            settings.CharacterId
        );
        ClientScene.AddPlayer(client.connection, 0, message);
        client.Send(PlayerJoinMessage.MESSAGE_TYPE, message);
        joined = true;
    }
}
