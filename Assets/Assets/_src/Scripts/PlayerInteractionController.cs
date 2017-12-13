using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInteractionController : NetworkBehaviour{


	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		
	}


	public void HandleAction (GameObject obj)
	{
		if(Input.GetMouseButtonUp(0)) //LMB release
		{
			CmdPlayerLeftClick (obj);
			
		} 
	}

	[Command]
	public void CmdPlayerLeftClick (GameObject obj)
	{
		RpcPlayerLeftClick (obj);
		
	}

	[ClientRpc]
	void RpcPlayerLeftClick (GameObject obj)
	{
		obj.GetComponent<InteractableObjectController> ().OnClick (); //Invoked on correspoding GameObjects on all clients
	}



	[Command]
	void CmdPlayerRightClick (GameObject obj)
	{

	}

	[ClientRpc]
	void RpcPlayerRightClick (GameObject obj)
	{

	}

}
