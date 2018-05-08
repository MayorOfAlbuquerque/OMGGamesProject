using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


public class CluePickupController : InteractableObjectController, IGvrPointerHoverHandler{

    private GameObject clueSpawner;
    public CluePlaceholder spec;

	// Use this for initialization
	void Start () {
        this.clueSpawner = GameObject.Find("ClueController");
        if(clueSpawner == null)
        {
            Debug.Log("---------------Clue Pickup controller cannot find clue controller object");
        }
        GetSpec();
    }
    //assign spec associated with this clue
    void GetSpec()
    {
        CluePlaceholder p = clueSpawner.GetComponent<ClueSpawner>().GetPlaceholderFromClue(this.gameObject);
        this.spec = p;
    }

    public override void OnKeyDown(KeyCode code) { }
    public override void OnKeyUp(KeyCode code) { }

    public override void OnClick() { }

    public override void OnClick(object obj)
    {
      
    }


    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hovering over clue------------------------------------------------");
    }


   
}
