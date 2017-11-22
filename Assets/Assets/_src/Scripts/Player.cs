
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
//using DoorController;


public class Player : NetworkBehaviour {

    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool> { }

    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleRemote;
    [SerializeField] ToggleEvent onToggleLocal;
    GameObject mainCamera;

    public float speed;

    private void Start()
        {
            mainCamera = Camera.main.gameObject;
            EnablePlayer();
        }

        private void EnablePlayer()
        {
            onToggleShared.Invoke(true);
            if (isLocalPlayer)
            {
                mainCamera.SetActive(false);
                onToggleLocal.Invoke(true);
            }
            else
            {
                onToggleRemote.Invoke(true);
            }
        }

        private void DisablePlayer()
        {
            onToggleShared.Invoke(false);
            if (isLocalPlayer)
            {
                mainCamera.SetActive(true);
                onToggleLocal.Invoke(false);
            }
            else
            {
                onToggleRemote.Invoke(false);
            }
        }


	/*
	 * Wrapper function for the open door command
	*/
	public void PlayerOpenDoor(GameObject door)
	{
		CmdOpenDoor (door);
	}

	/*
	 * Wrapper function for the close door command
	*/
	public void PlayerCloseDoor(GameObject door)
	{
		CmdCloseDoor (door);
	}

	/*
	 * Forces the server to open the door, this calls the RPC so animation is synced for all clients
	*/
	[Command]
	public void CmdOpenDoor(GameObject door){
		door.GetComponent<DoorController>().RpcOpenDoor (); //Synchronise this change for all other clients
	}
		

	/*
	 * Forces the server to close the door, this calls the RPC so animation is synced for all clients
	*/
	[Command]
	public void CmdCloseDoor(GameObject door){
		door.GetComponent<DoorController>().RpcCloseDoor ();
	}
		
}