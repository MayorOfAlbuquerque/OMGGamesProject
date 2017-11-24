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
        SetSpawnWeaponModel(currentWeapon);
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

        if(currentWeapon != Weapon.NONE)
        {
            Weapon weaponToBe = player.GetPlayerCurrentWeapon();
            player.SetPlayerWeapon(currentWeapon);
            SetSpawnWeaponModel(weaponToBe);
        }
    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hover over object");
    }

    private void SetSpawnWeaponModel(Weapon weapon)
    {
        this.currentWeapon = weapon;

        Destroy(currentModel);
        currentModel = null;
        Debug.Log(currentModel);

        if(weapon != Weapon.NONE)
        {
            Debug.Log("new model loading");
            //load object from resources folder
            currentModel = Instantiate(Resources.Load(weapon.ToString(), typeof(GameObject))) as GameObject;

            //set pickup spawner as parent. false flag sets transform relative to parent.
            currentModel.transform.SetParent(this.gameObject.transform, false);
        }

    }
}
