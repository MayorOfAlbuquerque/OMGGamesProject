using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingControllerScript : MonoBehaviour {

    int numberOfPlayersVoted = 0; //The Number of players that voted
	int numberOfPlayersConnected; //Current number of player connected (Detected)

	//List of players that have voted
    List<GameObject> playersList = new List<GameObject>();

	//Map of each player game object and WHO they voted for
    Dictionary<GameObject, int> playerVotes = new Dictionary<GameObject, int>(); 

	//Number of votes for player with id i  in listOfPlayerVotes[i]
	int[] listOfPlayerVotes = new int[10]; 
    

    private void Start() {
        for(int i = 0; i < listOfPlayerVotes.Length; i++){
            listOfPlayerVotes[0] = 0;
        }
		numberOfPlayersConnected = 0;
	}

    /* Function to allow the players to add votes. Also keeps track of gameobjects.
     * This is always called from a command, run on the server */
    public void PlayerAddVote(int uniqueId, GameObject thisPlayer, int player_numberOfConnects) {
		playersList.Add(thisPlayer);
		playerVotes.Add(thisPlayer, uniqueId);

		listOfPlayerVotes[uniqueId] += 1; //Increase the vote count for thier player choice
		numberOfPlayersVoted += 1; //Increase the number of players that have voted
        numberOfPlayersConnected = player_numberOfConnects;
		Debug.Log ("Player: " + thisPlayer + "just voted for" + uniqueId); //potato

		if (numberOfPlayersVoted == numberOfPlayersConnected){ //Check for game over
            Debug.Log("Players FINISHED VOTE from Voting Controller!");
            VotingEnded(); //Result
        }
    }
		
	/* Ends the game, fades out the players */
	void VotingEnded() {
    	/*  this is the placeholder if you want players to know if THEY voted correctly */
        foreach (GameObject player in playersList){
            Debug.Log("Calling End game for player : " + player);
			player.GetComponent<PlayerVotingSystem>().EndGame();
        }
    }

}
