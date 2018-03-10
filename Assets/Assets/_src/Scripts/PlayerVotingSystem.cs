using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
 *  This script is attached to the Player Prefab
 * 
 * 
 * */


public class PlayerVotingSystem : NetworkBehaviour {

    bool haveIVoted;
    bool didIWin;
    int uniquePlayerVotedFor;

    GameObject playerVotingAnchor;
    
    // Use this for initialization
    void Start () {
        uniquePlayerVotedFor = 0;
        playerVotingAnchor = GameObject.Find("PlayerVotingAnchor");
        if(playerVotingAnchor == null)
        {
            Debug.LogError("Player voting Anchor not found! ");
        }
        haveIVoted = false;
	}
    


    public void VoteForPlayer(int uniquePlayerId)
    {
       playerVotingAnchor.GetComponent<PlayerVotingController>().CmdUpdateVote(uniquePlayerId, haveIVoted,this.gameObject);
       if(haveIVoted == false) {
            haveIVoted = true;
            uniquePlayerVotedFor = uniquePlayerId;
        }
       Debug.Log("Players voted for unique id : "+uniquePlayerId);
    }

    public int PlayerVoted()
    {
        if(uniquePlayerVotedFor == 0 || haveIVoted == false)
        {
            Debug.Log("CAUTION : PlayerVoted doesn't return a correct value!");
            return -1;
        }
        return uniquePlayerVotedFor;
    }

    public void VotingResults(int idOfPlayerWithMostVotes)
    {
        if(idOfPlayerWithMostVotes == uniquePlayerVotedFor)
        {
            Debug.Log("PLAYER WON!");
            // This is a placeholder for something that actually needs to be done on winning
        }
        else
        {
            Debug.Log("PLAYER LOST!");
        }
    }

}
