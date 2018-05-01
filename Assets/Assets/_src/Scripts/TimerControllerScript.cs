using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerControllerScript : MonoBehaviour {

	List<GameObject> playerList = new List<GameObject>();
 

    [SerializeField]
    int maxPlayers;

    [SerializeField]
    public float taskLength = 30; //The specified job time

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
                Debug.Log(" ########### Timing down ######### " + remainingTime);
            }
            else if (remainingTime <= 0.0f)
            { //Check the server for completed timer
                complete = true;
                ForceTimerStop();
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
            Vector3 spawnToGive;
            if(lastOneGiven == 1)
            {
                spawnToGive = spawnTwo.transform.position;
                lastOneGiven = 2;
            }
            else if(lastOneGiven == 2)
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

}
