﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerServerManager))]
public class OMGNetManager : NetworkManager
{
    public PlayerServerManager playerManager;

    public override void OnStartServer()
    {
        playerManager.RegisterHandlers();

    }

    void Start()
    {
        playerManager = GetComponent<PlayerServerManager>();
    }

	public override void OnStartClient(NetworkClient client)
	{
        playerManager.RegisterPlayerPrefabs();
        playerManager.RegisterHandlers();
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        if(extraMessageReader == null) {
            return;
        }
        PlayerJoinMessage message = extraMessageReader.ReadMessage<PlayerJoinMessage>();
        if(message == null) {
            return;
        }
        playerManager.AddCharacter((int)message.characterId);
        CharacterSpec spec = playerManager
            .FindCharacterSpecById((int)message.characterId);
        GameObject player;
        if (spec != null)
        {  
            player = playerManager.InstantiateCharacter(spec, playerPrefab, GetStartPosition());
        }
        else
        {
            player = Instantiate(playerPrefab);
            player.transform.position = GetStartPosition().position;
        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        //rpc call to all clients to spawn clues with list of characters
        if(spec != null)
        {
            player.GetComponent<Player>().RpcSpawnPrivateClues(spec);
        }
    }



    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
    }
}

