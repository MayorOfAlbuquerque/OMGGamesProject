using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingControllerScript : MonoBehaviour {

    int[] listOfPlayerVotes = new int[10];
    int numberOfPlayersVoted = 0;
    List<GameObject> playersList = new List<GameObject>();

    int numberOfPlayersConnected;
	// Use this for initialization
	private void Start() {
		
        for(int i = 0; i < listOfPlayerVotes.Length; i++)
        {
            listOfPlayerVotes[0] = 0;
        }
	}

    public void PlayerAddVote(bool haveVoted, int uniqueId, GameObject thisPlayer, int player_numberOfConnects)
    {
        if(playersList.Contains(thisPlayer) != true)
        {
            playersList.Add(thisPlayer);
        }
        numberOfPlayersConnected = player_numberOfConnects;
        if (haveVoted == false)
        {
            listOfPlayerVotes[uniqueId] += 1;
            numberOfPlayersVoted += 1;
        }
        if (numberOfPlayersVoted == numberOfPlayersConnected)
        {
            VotingEnded();
        }
        else
        {
            UpdateVotes();
        }
    }

    public int GetPlayerVotes()
    {
        return numberOfPlayersVoted;
    }

    void VotingEnded()
    {
        foreach(GameObject player in playersList)
        {
            player.GetComponent<PlayerVotingSystem>().EndVotes();
        }
    }

    public bool HaveEndedVotes()
    {
        if(numberOfPlayersVoted != numberOfPlayersConnected)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void UpdateVotes()
    {
        foreach (GameObject player in playersList)
        {
            player.GetComponent<PlayerVotingSystem>().UpdateVotes(numberOfPlayersVoted);
        }
    }

}
