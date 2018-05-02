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
    OverlayFader my_fader;


    void Start()
    {

        Debug.Log("----- Start function for Timers! -------");
       
       

        TimerController = GameObject.FindGameObjectWithTag("TimerController");

        this_timerController = (TimerControllerScript)FindObjectOfType(typeof(TimerControllerScript));

        

        if(my_fader == null)
        {
            Debug.Log("Fader not found!");
        }  
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

    public void ShowCompleted(bool myTimerFinished, Vector3 TeleportPosition)
    {
        // Uncomment this when you're willing to move! 
        
        this.gameObject.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = TeleportPosition;
        my_fader.FadeToBlack();
        

        
    }

    public void ShowCompletedSecond()
    {
        my_fader.FadeToClear();
        this.gameObject.GetComponent<CharacterController>().enabled = true;
    }
    
    [Command]
    public void CmdRegisterThisPlayer(GameObject this_gameObj)
    {
       Debug.Log("Player " + this_gameObj + "is registering to the timers!  ");
       this_timerController.RegisterPlayer(this_gameObj);
    }


    [ClientRpc]
    public void RpcEndTimer(Vector3 TeleportPosition)
    {
        ShowCompleted(true, TeleportPosition);
    }

    [ClientRpc]
    public void RpcEndTimerSecond()
    {
        ShowCompletedSecond();
    }

    public void EndTimer(Vector3 TeleportPosition)
    {
        RpcEndTimer(TeleportPosition);
    }

    public void EndTimerSecond()
    {
        RpcEndTimerSecond();

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