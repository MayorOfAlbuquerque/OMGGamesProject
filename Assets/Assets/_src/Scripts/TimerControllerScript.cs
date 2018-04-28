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

    // Use this for initialization
    void Start () {
        remainingTime = taskLength;
	}
	
	// Update is called once per frame
	void Update () {

		if(shouldTimerStart == true)
        {
            if (remainingTime > 0.0f )
            { //Check the server for completed timer
                remainingTime = remainingTime - Time.deltaTime;
                //Debug.Log(" ########### Timing down ######### " + remainingTime);
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
            player.GetComponent<Timer>().EndTimer();
        }
    }


    public void RegisterPlayer(GameObject player)
    {
        if(playerList.Contains(player) == false)
        {
            playerList.Add(player);            
        }
        if (playerList.Count == maxPlayers)
        {
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
