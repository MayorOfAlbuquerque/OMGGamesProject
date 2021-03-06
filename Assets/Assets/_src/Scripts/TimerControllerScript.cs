﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerControllerScript : MonoBehaviour {

    [SerializeField]
    List<GameObject> playerList = new List<GameObject>();
 

    [SerializeField]
    int maxPlayers;

    [SerializeField]
    public float taskLength = 30; //The specified job time

    bool isFirstTimerDone = false;

    [SerializeField]
    public float secondTimerLength = 120;

    [SerializeField] //Toggle to pause and unpause the timer
    public bool shouldTimerStart = false;

    public float remainingTime; //Remaining job time

    [SerializeField]
    public bool complete = false;

    [SerializeField]
    public bool serverBlockTimer = false;

    [SerializeField]
    public int viewNumberOfConnectedPlayers = 0;


    [SerializeField]
    public GameObject spawnOne;

    [SerializeField]
    public GameObject spawnTwo;

    [SerializeField]
    public GameObject spawnThree;


    private int lastOneGiven = 1;

    // Use this for initialization
    void Start () {
        remainingTime = taskLength;
	}
	
	// Update is called once per frame
	void Update () {

        if(serverBlockTimer == true)
        {
            Debug.Log("timer on hold as server has halted time! ");
            return;
        }
		if(shouldTimerStart == true)
        {
            if (remainingTime > 0.0f )
            { //Check the server for completed timer
                remainingTime = remainingTime - Time.deltaTime;
                if (isFirstTimerDone)
                {
                    secondTimerLength -= Time.deltaTime;
                }
                Debug.Log(" ########### Timing down ######### ");
            }
            if (remainingTime <= 0.0f)
            { //Check the server for completed timer
                if(isFirstTimerDone == true)
                {
                    Debug.Log("------------- TIMER 2 ENDED -------------");
                    complete = true;
                    
                    BroadcastEndTimerSecond();
                    ForceTimerStop();
                    return;
                }

                isFirstTimerDone = true;
                Debug.Log("------------ TIMER 2 STARTING ---------------");
                remainingTime = secondTimerLength;
                secondTimerLength = 0;
                BroadcastEndTimer();
            }
        }
        else if(shouldTimerStart == false)
        {
            return;
        }
	}

    public void BroadcastEndTimer()
    {
        foreach (GameObject player in playerList)
        {
            try {

                Vector3 spawnToGive;
                if (lastOneGiven == 1)
                {
                    spawnToGive = spawnTwo.transform.position;
                    lastOneGiven = 2;
                }
                else if (lastOneGiven == 2)
                {
                    spawnToGive = spawnThree.transform.position;
                    lastOneGiven = 3;
                }
                else
                {
                    spawnToGive = spawnOne.transform.position;
                    lastOneGiven = 1;
                }
                player.GetComponent<Timer>().EndTimer(spawnToGive);
             }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }

    }


    public void BroadcastEndTimerSecond()
    {
        foreach (GameObject player in playerList)
        {
            try
            {
                player.GetComponent<Timer>().EndTimerSecond();
            }
            catch(System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }


    public bool IsTimerOneDone()
    {
        return isFirstTimerDone;
    }


    public void RegisterPlayer(GameObject player)
    {
        if(playerList.Contains(player) == false)
        {
            playerList.Add(player);
            Debug.Log("################### Added player " + player + " to the player list!");
            viewNumberOfConnectedPlayers += 1;
        }
        if (playerList.Count == maxPlayers)
        {
            Debug.Log("########################### Max players reached, Timer started! ");
            ForceTimerStart();
        }
		
    }

    public void ForceTimerStart()
    {
        shouldTimerStart = true;
    }

    public void ForceTimerStop()
    {
        shouldTimerStart = false;
    }

    public void ResetTimers()
    {
        taskLength = 30; //The specified job time
        isFirstTimerDone = false;
        secondTimerLength = 120;
        shouldTimerStart = true;
        remainingTime = 30; //Remaining job time
        complete = false;
        serverBlockTimer = false;


    }

    public void SetFirstTimer(int timerSeconds)
    {
        taskLength = timerSeconds;
        remainingTime = taskLength;
    }

    public void SetSecondTimer(int timerSeconds)
    {
        secondTimerLength = timerSeconds;
        if (isFirstTimerDone)
        {
            remainingTime = timerSeconds;
        }
        
    }

    public float GetFirstTimer()
    {
        return remainingTime;
    }

    public float GetSecondTimer()
    {
        if(isFirstTimerDone == true)
        {
            return remainingTime;
        }
        return secondTimerLength;
    }

    public void ServerForceStop()
    {
        serverBlockTimer = !serverBlockTimer;
    }



}


