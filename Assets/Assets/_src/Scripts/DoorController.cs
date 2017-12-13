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



	/// <summary>
	/// Runs on the server, opens the door for all the clients syncing the animation
	/// across the network.
	/// </summary>
	[ClientRpc] 
	public void RpcOpenDoor()
	{
		anim.Play("Opening", -1);
		isOpen = true;
		Debug.Log("Opened the door for all other clients");
	}
		

	/// <summary>
	/// Runs on the server, closes the door for all the clients syncing the animation
	/// across the network.
	/// </summary>
	[ClientRpc]
	public void RpcCloseDoor()
	{
		anim.Play("Closing", -1);
		isOpen = false;
		Debug.Log ("Closed the door for all other clients");
	}
		
	/// <summary>
	/// Runs on the server, since we want to RPC the door animation we have to get to the server side "Door"
	/// first, this command allows us to do this.
	/// </summary> 
	[Command]
	public void CmdCloseDoor()
	{
		RpcCloseDoor ();

	}

	/// <summary>
	/// Runs on the server, since we want to RPC the door animation we have to get to the server side "Door"
	/// first, this command allows us to do this.
	/// </summary> 
	[Command]
	public void CmdOpenDoor()
	{
		RpcOpenDoor ();
	}


	/*
	 * Toggles the relevant Open or close command, Toggle() is called by OnClick()
	 * TODO Condense the RPC into a single method as unity permits Bools as parameters for these calls
	*/
    public void Toggle() {
        if(isOpen) 
			CmdCloseDoor (); //RPC needs to be performed on the Server
        else
			CmdOpenDoor ();
    }
		
    #region Controller button handlers
    public override void OnClick()
    {
		Debug.Log ("Player Clicked");
        Toggle();
    }

    public override void OnKeyDown(KeyCode code) {}
    public override void OnKeyUp(KeyCode code) {}
    #endregion

    public bool IsOpen 
	{
		get
		{
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
