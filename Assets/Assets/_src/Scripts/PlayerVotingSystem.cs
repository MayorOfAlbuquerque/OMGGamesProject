using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerVotingSystem : NetworkBehaviour {

    int numOfConnectedPlayers;
    int[] listOfPlayersVotes = new int[10];
    
    int numberOfPlayersVoted=0;

    bool haveIVoted;
    bool haveInit = false;

 


    [Command]
    void CmdUpdateVote(int uniquePlayerId, bool haveIVotedPlayerInp)
    {
        VotingControllerScript this_Voting = (VotingControllerScript)FindObjectOfType(typeof(VotingControllerScript));
        numOfConnectedPlayers = NetworkManager.singleton.numPlayers;

        this_Voting.PlayerAddVote(haveIVotedPlayerInp, uniquePlayerId, this.gameObject, numOfConnectedPlayers);

    }
    

    public void VoteForPlayer(int uniquePlayerId)
    {
        if (haveInit == false)
        {
            haveIVoted = false;
            haveInit = true;
        }

       CmdUpdateVote(uniquePlayerId, haveIVoted);
       if(haveIVoted == false) { haveIVoted = true; }
       Debug.Log("Players voted for unique id : "+uniquePlayerId);
    }


    [ClientRpc]
    void RpcAnnounceResults()
    {
        Debug.Log("Have voted yo kids");
        // Win or Lose! Game ends and we're happy.
    }


    public void EndVotes()
    {
        RpcAnnounceResults();
    }

    public void UpdateVotes(int numVotes)
    {
        numberOfPlayersVoted = numVotes;
    }
}

