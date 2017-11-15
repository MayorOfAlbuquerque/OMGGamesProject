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
        if(GvrControllerInput.ClickButtonDown) {
            RaycastResult result = GvrPointerInputModule.CurrentRaycastResult;
            var interactable = result
                .gameObject
                .GetComponent<InteractableObjectController>() 
                    as InteractableObjectController;
            interactable?.OnClick();
        }
	}
}
