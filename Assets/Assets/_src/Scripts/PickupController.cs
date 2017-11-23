using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class PickupController : InteractableObjectController , IGvrPointerHoverHandler{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnKeyDown(KeyCode code) { }
    public override void OnKeyUp(KeyCode code) { }

    public override void OnClick()
    {
    }

    public override void OnClick(object obj)
    {
        Debug.Log("Player Clicked Object");
        Player player = obj as Player;
        //Get camera child, then box child of that. Set it active.
        player.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hover over object");
    }
}
