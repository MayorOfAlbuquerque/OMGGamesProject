using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Timer : NetworkBehaviour
{
    TimerControllerScript this_timerController;
    GameObject TimerController;
    bool haveReg = false;
    private Vector3 myNewPosition;


    [SerializeField]
    private GameObject Spawn1;

    [SerializeField]
    private GameObject Spawn2;

    [SerializeField]
    private GameObject Spawn3;


    private List<Vector3> spawns = new List<Vector3>();

    Vector3 mySpawn = new Vector3();

    void Start()
    {

        Debug.Log("----- Start function for Timers! -------");
        /*
        
        // Spawns commented out until read to put the spawns into the game 
        spawns.Add(Spawn1.transform.position);
        spawns.Add(Spawn2.transform.position);
        spawns.Add(Spawn3.transform.position);

        */
        if (isLocalPlayer == true)
        {
            Debug.Log("Local Player True! ");
            int randomInt = (int)Random.Range(0, spawns.Count);
            mySpawn = spawns[randomInt];
        }

        TimerController = GameObject.FindGameObjectWithTag("TimerController");

        this_timerController = (TimerControllerScript)FindObjectOfType(typeof(TimerControllerScript));

        if (TimerController == null)
        {
            Debug.Log("TimerController gameobject not found! ");
        }

        if (this_timerController == null)
        {
            if (TimerController != null) {
                this_timerController = TimerController.GetComponent<TimerControllerScript>();
            }
            else
            {
                Debug.Log("TimerController gameobject not found! ");
            }
            if(this_timerController == null)
            {
                Debug.Log("Timercontroller script not grabbed from the timerController obj");
            }
        }
        else
        {
            Debug.Log("Found the timercontroller! ");
            if (isLocalPlayer) {
                Debug.Log("Local player is now registering! ");
                CmdRegisterThisPlayer(this.gameObject);
          }
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (this_timerController == null)
        {
            this_timerController = (TimerControllerScript)FindObjectOfType(typeof(TimerControllerScript));
            Debug.Log("Tried to grab the object, in update, it's not present anywhere in scene");
        }
    }

    public void ShowCompleted(bool myTimerFinished)
    {
        // Uncomment this when you're willing to move! 
        
        /*
        this.gameObject.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = mySpawn;
        this.gameObject.GetComponent<CharacterController>().enabled = true;     
        */

        
    }
    
    [Command]
    public void CmdRegisterThisPlayer(GameObject this_gameObj)
    {
       Debug.Log("Player " + this_gameObj + "is registering to the timers!  ");
       this_timerController.RegisterPlayer(this_gameObj);
    }


    [ClientRpc]
    public void RpcEndTimer()
    {
        ShowCompleted(true);
    }

    public void EndTimer()
    {
        RpcEndTimer();
    }

    public void PlayerForceTimerStart()
    {
        this_timerController.ForceTimerStart();
    }
    public void PlayerForceTimerStop()
    {
        this_timerController.ForceTimerStop();
    }
}