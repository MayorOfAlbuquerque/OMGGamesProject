using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerVotingSystem : NetworkBehaviour {

    int numOfConnectedPlayers; //Number of connected players - Updated server side
	int votedId; //Who the player playing the attached game object has voted for


    bool haveIVoted;

	[SerializeField]
	public OverlayFader fader; //Fader for game end - Green=win; Red=Lose

    [SerializeField]
    public VotingControllerScript this_Voting; 

	private void Start()
    {
		//fader = GameObject.Find ("BlackOverlay")?.GetComponent<OverlayFader>();
		haveIVoted = false;
	}
 
    [Command]
    void CmdUpdateVote(int uniquePlayerId)
    {
       this_Voting = (VotingControllerScript) FindObjectOfType(typeof(VotingControllerScript));
        if(this_Voting == null)
        {
            Debug.Log("VOTING SCRIPT NOT FOUND! ");
        }
        numOfConnectedPlayers = NetworkManager.singleton.numPlayers;
        this_Voting.PlayerAddVote(uniquePlayerId, this.gameObject, numOfConnectedPlayers); //Monobehavior script existing on the server
    }
    
    public void VoteForPlayer(int uniquePlayerId)
    {
       if(haveIVoted == false)
        {
			votedId = uniquePlayerId; //update the local player
			CmdUpdateVote(uniquePlayerId); //Server side bookeeping
            haveIVoted = true; //Update the local player
        }
       Debug.Log("Player voted for unique id : "+uniquePlayerId);
    }

	[ClientRpc]
	void RpcCheckWin(bool didPlayersWin)
	{
		Debug.Log("Check win...");
		if (didPlayersWin)
        { 
                       
            //fader.FadeToGreen();
            Debug.Log("Fading to green");
            this.gameObject.GetComponent<CharacterController>().enabled = false;
            ExecuteEvents.Execute<IHelpTextDisplay>(
           this.gameObject,
           null,
           (x, y) =>
           {
               x.Show("The Innocent Players have won!");
           });
        }
		else
        {

            //fader.FadeToRed();
            this.gameObject.GetComponent<CharacterController>().enabled = false;
            ExecuteEvents.Execute<IHelpTextDisplay>(
           this.gameObject,
           null,
           (x, y) =>
           {
               x.Show("The Murderer has Won!");
           });

			Debug.Log("Fading to red");
		}
	}

	//Calls the RPC so each player can check if they won the game, avoids having to use targetRpc's
	public void EndGame(bool didPlayersWin)
    {
        Debug.Log("player " + this.gameObject + " is calling endgame ");
		RpcCheckWin(didPlayersWin);
	}

}



