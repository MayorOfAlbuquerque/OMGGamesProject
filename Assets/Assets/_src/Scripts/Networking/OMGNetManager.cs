using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerServerManager))]
public class OMGNetManager : NetworkManager
{
    public PlayerServerManager playerManager;
    public StoryServerManager storyServerManager;
    private string murderer;
    private GameObject clueController;
    private int storyNum, spawnedFlag =0;
    private Dictionary<String, GameObject> playerSpawnDict;
    public bool isServer = false;

    public override void OnStartServer()
    {
        isServer = true;
        playerManager.RegisterHandlers();
        storyServerManager?.ChooseStory();
        storyNum = storyServerManager?.CurrentStory.StoryId ?? 0;
        ChooseMurderer();
    }

    private void ChooseMurderer()
    {
        this.murderer = storyServerManager?.CurrentStory?.Murderer?.FullName;
    }

    private void AssignSpawns(int storyNum)
    {
        playerSpawnDict = new Dictionary<string, GameObject>();
        //get spawns for this game
        GameObject spawns = GameObject.Find("Spawns").transform.GetChild(0).gameObject;
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
        ChooseMurderer();
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
        if(playerManager.IsPlayerJoined(message.characterId)) {
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
        Debug.Log("network server adding player");
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