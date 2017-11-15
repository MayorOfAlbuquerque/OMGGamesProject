using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour, IGvrPointerHoverHandler {

    public DoorController door;

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("You looked something");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GvrControllerInput.ClickButtonDown) {
            Debug.Log("You click button in VR");
            door.Toggle();
        }
	}
}
