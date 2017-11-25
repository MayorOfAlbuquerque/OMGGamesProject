using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


public class PickupController : InteractableObjectController, IGvrPointerHoverHandler{

    [SerializeField]
    private Weapon currentWeapon;
    private GameObject currentModel;

	// Use this for initialization
	void Start () {
        AddModel(currentWeapon);
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
            //Needs altering for networking use
            player.SetPlayerWeapon(currentWeapon);
            player.CmdChangeSpawnWeapon(this.gameObject, weaponToBe);
        }
    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hover over object");
    }

    [ClientRpc]
    public void RpcSetSpawnWeaponModel(Weapon weaponToBe)
    {
        currentWeapon = weaponToBe;
        RemoveModel();
        
        if(weaponToBe != Weapon.NONE)
        {
            Debug.Log("new model loading");
            AddModel(weaponToBe);
        }

    }

    private void RemoveModel()
    {
        Destroy(currentModel);
        currentModel = null;
        Debug.Log("Current model = " + currentModel);

    }

    private void AddModel(Weapon weapon)
    {
        //load object from resources folder
        currentModel = Instantiate(Resources.Load(weapon.ToString(), typeof(GameObject))) as GameObject;

        //set pickup spawner as parent. false flag sets transform relative to parent.
        currentModel.transform.SetParent(this.gameObject.transform, false);
    }
}
