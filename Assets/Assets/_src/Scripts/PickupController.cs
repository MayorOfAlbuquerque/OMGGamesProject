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
    }


    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hovering over weapon");
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
