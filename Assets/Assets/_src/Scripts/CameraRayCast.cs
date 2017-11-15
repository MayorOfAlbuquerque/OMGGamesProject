using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRayCast : MonoBehaviour, IGvrPointerHoverHandler {
    public GvrReticlePointer pointer;

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Pointer pointing at something");
        GameObject pointed = eventData.pointerCurrentRaycast.gameObject;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
