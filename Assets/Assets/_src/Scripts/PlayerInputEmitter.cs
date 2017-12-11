using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputEmitter : MonoBehaviour {
    

    // Use this for initialization
    void Start () {
        //Get the player reference here as this wont change
	}
	
	// Update is called once per frame
	void Update () {
        RaycastResult result = GvrPointerInputModule.CurrentRaycastResult;
            var interactable = result
                .gameObject?
                .GetComponent<InteractableObjectController>() 
				as InteractableObjectController;


		//do a switch statement heremfor input
		if (interactable) {
			GameObject obj = result.gameObject as GameObject;
			if(Input.GetMouseButtonUp(0)) {
				Player player = gameObject.GetComponent<Player> (); //Get the player that clicked, dont want to do this every fram
				player.CmdInteractObject(obj);
			} else if(Input.GetMouseButtonDown(1)) {
				//     interactable?.OnKeyDown(KeyCode.Mouse1);
			} else if(Input.GetMouseButtonUp(1)) {
				//     interactable?.OnKeyUp(KeyCode.Mouse1);
			}
		} 
		else 
		{
			return;
		}
      
	}
}
