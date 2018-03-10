using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  This script is attached to the Voting Box type Object
 * 
 * 
 * */


public class VotingBoxScript : MonoBehaviour {

    [SerializeField] public int playerRepId; // Which player the box represents as the murderer.

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("someone walked all over me " + playerRepId );
            other.GetComponent<PlayerVotingSystem>().VoteForPlayer(playerRepId);
        }
    }
}
