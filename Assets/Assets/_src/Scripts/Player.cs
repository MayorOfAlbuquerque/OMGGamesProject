
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



	public void PlayerOpenDoor(GameObject door)
	{
		CmdOpenDoor (door);
	}

	public void PlayerCloseDoor(GameObject door)
	{
		CmdCloseDoor (door);
	}

	[Command]
	public void CmdOpenDoor(GameObject door){
		door.GetComponent<DoorController>().RpcOpenDoor (); //Synchronise this change for all other clients
	}
		

	[Command]
	public void CmdCloseDoor(GameObject door){
		door.GetComponent<DoorController>().RpcCloseDoor ();
	}
		


	/*[ClientRpc]
	public void RpcCloseDoor()
	{
		if (isLocalPlayer)
			return;
		DoorController.Close(); //Animate the door
		Debug.Log ("Closed the door for all other clients");
	}

	[ClientRpc]
	public void RpcOpenDoor()
	{
		if (isLocalPlayer)
			return;
		DoorController.Open(); //Animate the door
		Debug.Log ("Closed the door for all other clients");
	}
	*/
		
}