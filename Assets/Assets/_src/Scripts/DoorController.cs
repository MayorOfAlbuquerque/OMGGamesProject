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
		Open ();
		Debug.Log("Opened the door for all other clients");
	}
		
	/// <summary>
	/// Runs on the server, closes the door for all the clients syncing the animation
	/// across the network.
	/// </summary>
	[ClientRpc]
	public void RpcCloseDoor()
	{
		Close ();
		Debug.Log ("Closed the door for all other clients");
	}

	//TODO Condense the RPC into a single method as unity permits Bools as parameters for these calls
	/// <summary>
	/// Toggles the relevant Open or close command, Toggle() is called by OnClick() 
	/// </summary>
    public void Toggle() {
		if (isOpen) 
		{
			this.Close();	//If we only use RPC, the action does not occur on the server, this is potentially an issue if we need to query the server for informations
			RpcCloseDoor ();
		} 
		else 
		{
			this.Open();
			RpcOpenDoor ();
		}
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
