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

	/// <summary>
	/// Method that handles player interaction with an interactable gameobject
	/// </summary>
	/// <param name="obj">The game object the player is trying to interact with</param> 
	public void HandleAction (GameObject obj)
	{
		if(Input.GetMouseButtonUp(0)) //LMB release
		{
			CmdPlayerLeftClick (obj);
			
		} 
		//Extend this as necessary
	}


	/// <summary>
	/// Runs on the server, allowing the calling of Rpc's to display animation to all clients. 
	/// </summary>
	/// <param name="obj">The game object the player is trying to interact with</param> 
	[Command]
	public void CmdPlayerLeftClick (GameObject obj)
	{
		obj.GetComponent<InteractableObjectController> ().OnClick (); //Needs to run on server as players do not have authority over interactable objects
		
	}
		

	/// <summary>
	/// Runs on the server, allowing the calling of Rpc's to display animation to all clients. 
	/// </summary>
	/// <param name="obj">The game object the player is trying to interact with</param> 
	[Command]
	void CmdPlayerRightClick (GameObject obj)
	{

	}

}
