using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputEmitter : MonoBehaviour {
    

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        RaycastResult result = GvrPointerInputModule.CurrentRaycastResult;
            var interactable = result
                .gameObject?
                .GetComponent<InteractableObjectController>() 
                    as InteractableObjectController;
        if(Input.GetMouseButtonUp(0)) {
            interactable?.OnClick();
			Player player = gameObject.GetComponent<Player> (); //Get the player that clicked
			interactable?.OnClick(player);
        } else if(Input.GetMouseButtonDown(1)) {
            interactable?.OnKeyDown(KeyCode.Mouse1);
        } else if(Input.GetMouseButtonUp(1)) {
            interactable?.OnKeyUp(KeyCode.Mouse1);
        }
	}
}
