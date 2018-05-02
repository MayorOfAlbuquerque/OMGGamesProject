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
    private Dictionary<String, GameObject> playerSpawnDict;

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

    private void AssignSpawns(int storyNum)
    {
        playerSpawnDict = new Dictionary<string, GameObject>();
        //get spawns for this game
        GameObject spawns = GameObject.Find("Spawns").transform.GetChild(storyNum-1).gameObject;
        if(spawns != null)
        {   
            //loop through spawns and assign to character
            for(int i = 0; i < 6; i++)
            {
                GameObject individualSpawn = spawns.transform.GetChild(i).gameObject;
                playerSpawnDict.Add(individualSpawn.gameObject.name, individualSpawn);
            }  
        }
        else
        {
            Debug.Log("No spawns found");
        }
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
            this.clueController.GetComponent<ClueSpawner>().SpawnGeneralClues();
            spawnedFlag = 1;
        }
    }

    public override void OnStartClient(NetworkClient client)
	{
        playerManager.RegisterPlayerPrefabs();
        playerManager.RegisterHandlers();
	}

    private Transform GetPlayerSpawn(String playerName)
    {
        GameObject outVal;
        Debug.Log("______" + playerName);
        playerSpawnDict.TryGetValue(playerName, out outVal);
        return outVal.transform;
    }

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        if(playerSpawnDict == null)
        {
            AssignSpawns(storyNum);
        }
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
            player = playerManager.InstantiateCharacter(spec, playerPrefab, GetPlayerSpawn(spec.FullName));
        }
        else
        {
            player = Instantiate(playerPrefab);
            player.transform.position = GetStartPosition().position;
        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        //rpc call to all clients to assign info to prefabs
        if(spec != null)
        {
            SpawnAllServerAndClientClues(player);
            if (spec.FullName == this.murderer)
            {
                player.GetComponent<Player>().RpcSetInformation(spec, true);
                player.GetComponent<Player>().SetMurderer(true);
            }
            else
            {
                player.GetComponent<Player>().RpcSetInformation(spec, false);
            }
        }
    }

    void OnClientClueSceneLoaded() {
        
    }
    void OnClientModelSceneLoaded() {
        
    }
}