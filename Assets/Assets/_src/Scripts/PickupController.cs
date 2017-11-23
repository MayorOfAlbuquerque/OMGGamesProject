using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class PickupController : InteractableObjectController , IGvrPointerHoverHandler{

    [SerializeField]
    private Weapon currentWeapon;

	// Use this for initialization
	void Start () {
        SetWeaponModel(currentWeapon);
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

        if (player.GetPlayerCurrentWeapon() == Weapon.NONE) {
            this.gameObject.SetActive(false);
        }
        /*
        Transform tr = this.gameObject.transform;

        foreach(Transform child in tr)
        {
            child.gameObject.SetActive(false);
        }*/
        
    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hover over object");
    }

    private void SetWeaponModel(Weapon weapon)
    {

    }
}
