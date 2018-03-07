using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerVotingSystem : NetworkBehaviour {

    int numOfConnectedPlayers;
    int[] listOfPlayersVotes = new int[10];
    
    int numberOfPlayersVoted;

    bool haveIVoted;
    

    // Use this for initialization
    void Start () {
        numOfConnectedPlayers = NetworkManager.singleton.numPlayers;
        Debug.Log("Number of Players : " + numOfConnectedPlayers);
        for (int i = 0; i < numOfConnectedPlayers; i++)
        {
            listOfPlayersVotes[i] = 0;
        }

        haveIVoted = false;
        numberOfPlayersVoted = 0;
	}

    // Update is called once per frame
    void Update () {
        
        numOfConnectedPlayers = NetworkManager.singleton.numPlayers - 1;
        
        Debug.Log("Number of Players : " + numOfConnectedPlayers);
    }

    [Command]
    void CmdUpdateVote(int uniquePlayerId, bool haveIVoted)
    {
        if (haveIVoted == false)
        {
            listOfPlayersVotes[uniquePlayerId] += 1;
            numberOfPlayersVoted += 1;
            Debug.Log("Number Of Players Voted " + numberOfPlayersVoted);
        }

        if (numberOfPlayersVoted == numOfConnectedPlayers)
        {
            Debug.Log("ALL PLAYERS VOTED! ");
            RpcAnnounceResults();
        }
    }


    public void VoteForPlayer(int uniquePlayerId)
    {
       Debug.Log("Number of Players : " + numOfConnectedPlayers);
       CmdUpdateVote(uniquePlayerId, haveIVoted);
       if(haveIVoted == false) { haveIVoted = true; }
        Debug.Log("Players voted for unique id : "+uniquePlayerId);
    }
    


    [ClientRpc]
    public void RpcAnnounceResults()
    {
        Debug.Log("Have voted yo kids");
        // Win or Lose! Game ends and we're happy.
    }

}
