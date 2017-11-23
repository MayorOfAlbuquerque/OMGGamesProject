using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


public class PickupController : InteractableObjectController , IGvrPointerHoverHandler{

    [SerializeField]
    private Weapon currentWeapon;
    private GameObject currentModel;

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

        if (player.GetPlayerCurrentWeapon() == Weapon.NONE)
        {
            Resources.UnloadAsset(currentModel);
        }
        player.SetPlayerWeapon(currentWeapon);

    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hover over object");
    }

    private void SetWeaponModel(Weapon weapon)
    {
        this.currentWeapon = weapon;
        //load object from resources folder
        currentModel = Instantiate(Resources.Load(weapon.ToString(), typeof(GameObject))) as GameObject;

        //set pickup spawner as parent. false flag sets transform relative to parent.
        currentModel.transform.SetParent(this.gameObject.transform, false);

    }
}
