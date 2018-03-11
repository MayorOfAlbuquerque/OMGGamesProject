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
        Debug.Log(numberOfPlayersVoted);
        if(haveInit == false)
        {
            numOfConnectedPlayers = NetworkManager.singleton.numPlayers;
            Debug.Log("Number of players connected :"+numOfConnectedPlayers);
            Debug.Log("Number of players voted :" + numberOfPlayersVoted);
            for (int i = 0; i < numOfConnectedPlayers; i++)
            {
                listOfPlayersVotes[i] = 0;
            }
            haveInit = true;
        }

        if (haveIVotedPlayerInp == false)
        {
            Debug.Log("PLAYER HAS VOTED");
            listOfPlayersVotes[uniquePlayerId] += 1;
            numberOfPlayersVoted += 1;
        }


        Debug.Log("Number of players connected AFTER INIT:" + numOfConnectedPlayers);
        Debug.Log("Number of players voted AFTER INIT:" + numberOfPlayersVoted);

        if (numberOfPlayersVoted == numOfConnectedPlayers)
        {
            Debug.Log("ALL PLAYERS VOTED! ");
            RpcAnnounceResults();
        }

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

}
