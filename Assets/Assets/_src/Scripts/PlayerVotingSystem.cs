﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerVotingSystem : NetworkBehaviour {

    int numOfConnectedPlayers; //Number of connected players - Updated server side
	int votedId; //Who the player playing the attached game object has voted for

    [SerializeField]
    public int MurdererID = 0; //What is the correct answer

	//Bookeeping variables
    bool haveIVoted;

	[SerializeField]
	public OverlayFader fader; //Fader for game end - Green=win; Red=Lose

	//Initialise what we need
	private void Start(){
		//fader = GameObject.Find ("BlackOverlay")?.GetComponent<OverlayFader>();
		haveIVoted = false;
	}
 
	//Run on the server side version of VotingControllerScript, totals number of votes, ends the game etc.
    [Command]
    void CmdUpdateVote(int uniquePlayerId) {
        VotingControllerScript this_Voting = (VotingControllerScript)FindObjectOfType(typeof(VotingControllerScript));
        numOfConnectedPlayers = NetworkManager.singleton.numPlayers;
        this_Voting.PlayerAddVote(uniquePlayerId, this.gameObject, numOfConnectedPlayers); //Monobehavior script existing on the server
    }
    
	//Method called when player enters a voting box
    public void VoteForPlayer(int uniquePlayerId) {
       if(haveIVoted == false) {
			votedId = uniquePlayerId; //update the local player
			CmdUpdateVote(uniquePlayerId); //Server side bookeeping
            haveIVoted = true; //Update the local player
        }
       Debug.Log("Player voted for unique id : "+uniquePlayerId);
    }

	//Each player checks the murdererId against who they voted for and fades accordingly
	[ClientRpc]
	void RpcCheckWin()
	{
		Debug.Log ("Is Murderer ID correct ----> " + MurdererID);
		Debug.Log("Check win...");
		if (votedId == MurdererID){ //Just using local values here since we have them anway, why bother going through the server
			fader.FadeToGreen();
			Debug.Log("Fading to green");
		}
		else{
			fader.FadeToRed();
			Debug.Log("Fading to red");
		}
	}

	//Calls the RPC so each player can check if they won the game, avoids having to use targetRpc's
	public void EndGame(){
        Debug.Log("player " + this.gameObject + " is calling endgame ");
		RpcCheckWin();
	}

}



