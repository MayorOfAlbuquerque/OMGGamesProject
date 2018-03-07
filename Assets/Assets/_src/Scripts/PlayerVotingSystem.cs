using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerVotingSystem : NetworkBehaviour {

    int numOfConnectedPlayers = NetworkManager.singleton.numPlayers;
    int[] listOfPlayersVotes = new int[10];
    
    int numberOfPlayersVoted;

    bool haveIVoted;
    

    // Use this for initialization
    void Start () {
		for(int i = 0; i < numOfConnectedPlayers; i++)
        {
            listOfPlayersVotes[i] = 0;
        }

        haveIVoted = false;
        numberOfPlayersVoted = 0;
	}
	
	// Update is called once per frame
	void Update () {

       
	}
    
    [Command]
    void CmdUpdateVote(int uniquePlayerId, bool haveIVoted)
    {
        if (haveIVoted == false)
        {
            listOfPlayersVotes[uniquePlayerId] += 1;
            numberOfPlayersVoted += 1;
        }

        if (numberOfPlayersVoted == numOfConnectedPlayers)
        {
            RpcAnnounceResults();
        }

    }
    

    void VoteForPlayer(int uniquePlayerId)
    {
       CmdUpdateVote(uniquePlayerId, haveIVoted);
       if(haveIVoted == false) { haveIVoted = true; }

    }

    [ClientRpc]
    void RpcAnnounceResults()
    {
        Debug.Log("Have voted yo kids");
        // Win or Lose! Game ends and we're happy.
    }

}
