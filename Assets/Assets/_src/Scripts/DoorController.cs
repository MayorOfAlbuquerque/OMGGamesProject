using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
public class DoorController : InteractableObjectController, IDoor, IGvrPointerHoverHandler{

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
	
    public void Open() {
        anim.Play("Opening", -1);
        isOpen = true;
    }

    public void Close() {
        anim.Play("Closing", -1);
        isOpen = false;
    }

    public void Toggle() {
        if(isOpen) {
            Close();
        }
        else {
            Open();
        }
    }
    #region Controller button handlers
    public override void OnClick()
    {
        Toggle();
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
