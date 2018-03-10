using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerClientManager : MonoBehaviour {

    public bool joinOnSceneLoad;
    public GameSettings settings;
    private NetworkClient client;
    private bool joined = false;

    public void Start()
    {
        client = NetworkManager.singleton?.client;
        if(joinOnSceneLoad) {
            //JoinServer();
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
        PlayerJoinMessage message = new PlayerJoinMessage(
            client.connection.connectionId,
            settings.CharacterId
        );
        ClientScene.AddPlayer(client.connection, 0, message);
        //client.Send(PlayerJoinMessage.MESSAGE_TYPE, message);
        joined = true;
    }
}
