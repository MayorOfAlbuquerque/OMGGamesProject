using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class OMGNetManager : NetworkManager
{
    public PlayerServerManager playerManager;

    public override void OnStartServer()
    {
        playerManager.RegisterHandlers();
        playerManager.RegisterPlayerPrefabs();
    }
    
    void Start()
    {
        playerManager = GetComponent<PlayerServerManager>();
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
            player = playerManager.InstantiateCharacter(spec, playerPrefab);
        }
        else
        {
            player = Instantiate(playerPrefab);
        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        //rpc call to all clients to spawn clues with list of characters
       // GameObject.Find("ClueController").GetComponent<ClueSpawner>().RpcSpawnPrivateClues(playerManager.list.Characters);
    }



    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }
}

