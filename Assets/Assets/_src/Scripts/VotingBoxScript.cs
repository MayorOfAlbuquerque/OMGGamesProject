using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingBoxScript : MonoBehaviour {

    [SerializeField] public int playerRepId; // Which player the box represents as the murderer.

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("A player voted for " + playerRepId );
            other.GetComponent<PlayerVotingSystem>().VoteForPlayer(playerRepId);
        }
    }
}
