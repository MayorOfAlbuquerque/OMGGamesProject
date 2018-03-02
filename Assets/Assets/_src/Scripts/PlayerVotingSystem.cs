using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerVotingSystem : NetworkBehaviour {

    var numOfConnectedPlayers = Network.connections.Length;
    int[] listOfPlayersVotes = new int[numOfConnectedPlayers];
    public CharacterList playerCharacterList;  

    int numberOfPlayersVoted;

    bool haveIVoted;
    

    /*
    * 
    *  For Dynamic playableCharacters, 
    *  use the PlayerServerManager from Zaiyang to get the dynamic number of players!
    * 
    * */



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
    bool updateVote(int uniquePlayerId, bool haveIVoted)
    {
        if (haveIVoted == false)
        {
            listOfPlayersVotes[uniquePlayerId] += 1;
            numberOfPlayersVoted += 1;
        }

        if (numberOfPlayersVoted == numOfConnectedPlayers)
        {
            announceResults();
        }

        return true;

    }

    [ClientRpc]
    void voteForPlayer(int uniquePlayerId)
    {
        haveIVoted = updateVote(uniquePlayerId, haveIVoted);
    }

    void announceResults()
    {
        // Win or Lose! Game ends and we're happy.
    }

}
