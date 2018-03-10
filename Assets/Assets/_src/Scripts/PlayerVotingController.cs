using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/*
 *  This script is attached to the GameObject that's in the scene
 *  without needing it to be managed by a player specifically.
 * 
 * 
 * */



public class PlayerVotingController : NetworkBehaviour {

    int numOfConnectedPlayers= 0;
    int[] listOfPlayersVotes = new int[10];
    List<GameObject> playersList = new List<GameObject>();
    int numberOfPlayersVoted = 0;

    private int GetResults(int numberOfPlayersVoted)
    {
        int max_id = 0;
        int currentMax = 0;
        for (int i = 0; i <numberOfPlayersVoted; i++){
            if(listOfPlayersVotes[i] > currentMax)
            {
                currentMax = listOfPlayersVotes[i];
                max_id = i;
            }
        }
        return max_id;
    }

    [Command]
    public void CmdUpdateVote(int uniquePlayerId, bool haveIVoted, GameObject player)
    {
        numOfConnectedPlayers = NetworkManager.singleton.numPlayers - 1;

        if (playersList == null) // basically the OnStart stage
        {
            playersList = new List<GameObject>();
            Debug.Log("Setting the player votes to 0");
            for (int i = 0; i < numOfConnectedPlayers; i++)
            {
                listOfPlayersVotes[i] = 0;
            }
        }
        if (playersList.Contains(player) == false)
        {
            playersList.Add(player);
            // Add a function that adds it to local players
        }
        if (haveIVoted == false)
        {
            listOfPlayersVotes[uniquePlayerId] += 1;
            numberOfPlayersVoted += 1;
            Debug.Log("Number Of Players Voted " + numberOfPlayersVoted);
        }

        if (numberOfPlayersVoted == numOfConnectedPlayers)
        {
            int whoGotSelected = GetResults(numberOfPlayersVoted);
            Debug.Log("ALL PLAYERS VOTED! ");
            RpcAnnounceResults(whoGotSelected);
        }
    }

    [ClientRpc]
    public void RpcAnnounceResults( int votedId) // don't send lists. Send arrays
    {
        foreach(GameObject player in playersList)
        {
            player.GetComponent<PlayerVotingSystem>().VotingResults(votedId);
        }

        Debug.Log("######## FINISHED ANNOUNCING RESULTS ##########");

    }

    [ClientRpc]
    public void RpcAddPlayerToList(GameObject Player)
    {
        if (playersList == null) // basically the OnStart stage
        {
            playersList = new List<GameObject>();
        }
            playersList.Add(Player);
    }
}
    
