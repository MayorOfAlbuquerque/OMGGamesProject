using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// Class that handles any player interations that should be networked, 
/// particularly animations that need to be shown for every other player in the game.
/// </summary>
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
			PlayerLeftClick (obj);
			
		} 
		//Extend this as necessary
	}


	[Command]
	public void PlayerLeftClick (GameObject obj)
	{
		obj.GetComponent<InteractableObjectController> ().OnClick (); //Needs to run on server as players do not have authority over interactable objects
		
	}
		

	[Command]
	void CmdPlayerRightClick (GameObject obj)
	{

	}

}
