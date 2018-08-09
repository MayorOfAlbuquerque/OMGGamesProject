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

    public override void OnClick() { }

    public override void OnClick(object obj)
    {
        Debug.Log("Player Clicked Object");
        Player player = obj as Player;

        if(currentWeapon != Weapon.NONE)
        {
            Weapon newWeapon = player.GetPlayerCurrentWeapon();
            
            player.SetPlayerWeapon(currentWeapon);
            RemoveModel();
            if (newWeapon != Weapon.NONE)
            {
                AddModel(newWeapon);
            }
            currentWeapon = newWeapon;
            RpcSetSpawnWeaponModel(newWeapon); 
        }
        else
        {
            Debug.Log("No weapon to pick up");
        }
    }


    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hovering over weapon");
    }

    [ClientRpc]
    public void RpcSetSpawnWeaponModel(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        RemoveModel();
        
        if(newWeapon != Weapon.NONE)
        {
            Debug.Log("New model added");
            AddModel(newWeapon);
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
        Debug.Log("new model loading");
        //load object from resources folder
        currentModel = Instantiate(Resources.Load(weapon.ToString(), typeof(GameObject))) as GameObject;

        //set pickup spawner as parent. false flag sets transform relative to parent.
        currentModel.transform.SetParent(this.gameObject.transform, false);
    }
}
