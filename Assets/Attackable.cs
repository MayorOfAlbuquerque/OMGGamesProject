using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class Attackable : InteractableObjectController, IGvrPointerHoverHandler { 

    // Use this for initialization
    void Start () {
		
	}

    public override void OnKeyDown(KeyCode code) { }
    public override void OnKeyUp(KeyCode code) { }

    public override void OnClick()
    {
    }


    public void OnGvrPointerHover(PointerEventData eventData)
    {
    }


    public override void OnClick(object obj)
    {
        Debug.Log("You have attacked player");
        Player player = obj as Player;

        //get player weapon
        //request server remove health from other player
        player.CmdAttackPlayer(this.gameObject);
    }


   
}
