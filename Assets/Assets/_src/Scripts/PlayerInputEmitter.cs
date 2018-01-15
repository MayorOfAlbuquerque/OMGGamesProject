using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputEmitter : MonoBehaviour {
    
	private PlayerInteractionController playerInteractionController;

    // Use this for initialization
    void Start () 
	{
		playerInteractionController = gameObject.GetComponent<PlayerInteractionController> (); // Get the player reference here as this wont change
	}
	
	// Update is called once per frame
	void Update () {
        RaycastResult result = GvrPointerInputModule.CurrentRaycastResult; // Get the Raycast each frame
		if (result.gameObject?.GetComponent<InteractableObjectController>()) // Can we interact with the object?
		{
			GameObject obj = result.gameObject as GameObject;
			playerInteractionController.HandleAction (obj); // If we can, handle the request 
		}
	}
}