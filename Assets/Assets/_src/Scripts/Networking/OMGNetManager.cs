using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerServerManager))]
public class OMGNetManager : NetworkManager
{
    public PlayerServerManager playerManager;
    private string murderer;
    private GameObject clueController;
    private int storyNum, spawnedFlag =0;

    public override void OnStartServer()
    {
        playerManager.RegisterHandlers();
        this.storyNum = SelectStory();
    }
    //choose story with random int
    private int SelectStory()
    {
        return UnityEngine.Random.Range(1, 1);
    }
    
    //TODO:
    private void ChooseMurderer(int storyNum)
    {
        //get murderer name from story
        string mName = "Ms Scarlett";
        this.murderer = mName;
    }

    void Start()
    {
        playerManager = GetComponent<PlayerServerManager>();
        ChooseMurderer(storyNum);
    }

    private void SpawnAllServerAndClientClues(GameObject player)
    {   
        if(spawnedFlag ==0)
        {
            this.clueController = GameObject.Find("ClueController");
            //server set clues
            this.clueController.GetComponent<ClueSpawner>().SpawnGeneralClues(storyNum);
            spawnedFlag = 1;
        }
        //RPC spawn player clues
        player.GetComponent<Player>().RpcSpawnClues(storyNum);
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
            SpawnAllServerAndClientClues(player);
            if (spec.FullName == this.murderer)
            {
                player.GetComponent<Player>().RpcSpawnPrivateClues(spec, true);
                player.GetComponent<Player>().SetMurderer(true);
            }
            else
            {
                player.GetComponent<Player>().RpcSpawnPrivateClues(spec, false);
            }
        }
    }



    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
    }
}

