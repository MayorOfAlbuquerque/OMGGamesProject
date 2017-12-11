using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
//using Player;

public interface IDoor
{
    void Open();
    void Close();
    void Toggle();
    bool IsOpen { get; }

}

/// <summary>
/// Door controller will play the correct open/close animation and the store 
/// the current state of the door. (open/close)
/// </summary>
[RequireComponent(typeof(Animator))]
public class DoorController : InteractableObjectController, IDoor, IGvrPointerHoverHandler {

    public bool openInitially;
    private bool isOpen;
    public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();  
        isOpen = openInitially;
        if(isOpen) {
            Open();
        }
	}

	public void AnimateDoor()
	{
		if(IsOpen)
			anim.Play("Opening", -1);
		else
			anim.Play("Closing", -1);
	}
		
    public void Open() {
        anim.Play("Opening", -1);
        isOpen = true;
    }

	public void Close() {
		anim.Play("Closing", -1);
		isOpen = false;
	}


	/*
	 * Runs on the server, opens the door for all the clients, syncing the animation
	 * across the network.
	*/
	[ClientRpc] 
	public void RpcOpenDoor()
	{
		anim.Play("Opening", -1);
		isOpen = true;
		Debug.Log("Opened the door for all other clients");
	}

	/*
	 * Runs on the server, closes the door for all the clients, syncing the animation
	 * across the network.
	*/
	[ClientRpc]
	public void RpcCloseDoor()
	{
		anim.Play("Closing", -1);
		isOpen = false;
		Debug.Log ("Closed the door for all other clients");
	}


	/*
	 * This is temporarily redundant code
	*/
    public void Toggle() {
        if(isOpen) 
		{
			anim.Play("Closing", -1);
			isOpen = false;
			Debug.Log ("Closed the Door");
        }
        else
		{
			anim.Play("Opening", -1);
			isOpen = true;
			Debug.Log("Opened the Door");
        }
    }


	/*
	 * Calls the methods on the player script, this has local authority so can call commands
	 */ 
	public void PlayerToggle(Player player)
	{
		if(isOpen) {
			player.PlayerCloseDoor(this.gameObject);
		}
		else {
			player.PlayerOpenDoor(this.gameObject);
		}
	}


    #region Controller button handlers
    public override void OnClick()
    {
		Debug.Log ("Player Clicked");
        Toggle();
		//PlayerToggle();
    }

	public override void OnClick(object obj)
	{
		Debug.Log ("Player Clicked");
		Player player = obj as Player;
		PlayerToggle(player);
	}
    public override void OnKeyDown(KeyCode code) {}
    public override void OnKeyUp(KeyCode code) {}
    #endregion

    public bool IsOpen {
        get {
            return isOpen;
        }   
    }

    private void OnMouseEnter()
    {
        GlowingInteractable[] glowingObj = GetComponentsInChildren<GlowingInteractable>();
        foreach(var o in glowingObj) {
            o.SetGlowing();
        }
    }

    private void OnMouseExit()
    {
        GlowingInteractable[] glowingObj = GetComponentsInChildren<GlowingInteractable>();
        foreach (var o in glowingObj)
        {
            o.SetNormal();
        }
    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        
    }

}
