using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputEmitter : MonoBehaviour {
    
	private PlayerInteractionController playerInteractionController;
    private GameObject currentlyHovered = null;
    // Use this for initialization
    void Start () 
	{
		playerInteractionController = gameObject.GetComponent<PlayerInteractionController> (); // Get the player reference here as this wont change
	}
	
	// Update is called once per frame
	void Update () {
        RaycastResult result = GvrPointerInputModule.CurrentRaycastResult; // Get the Raycast each frame
        if (result.gameObject == null)
        {
            if (currentlyHovered != null)
            {
                playerInteractionController.HandleOnHoverExitAction(currentlyHovered, ref result);
                currentlyHovered = null;
            }
            return;
        }
        else
        {
            if (currentlyHovered != null)
            {
                playerInteractionController.HandleOnHoverExitAction(currentlyHovered, ref result);
            }
            currentlyHovered = result.gameObject;
            playerInteractionController.HandleOnHoverEnterAction(currentlyHovered, ref result);
        }

		if (result.gameObject?.GetComponent<InteractableObjectController>()) // Can we interact with the object?
		{
			GameObject obj = result.gameObject as GameObject; //Get the object that the raycast hit
            playerInteractionController.HandleOnClickAction (obj, ref result); // If we can, handle the request (This is what the raycasting player does)
		}
	}
}