using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Renderer))]
public class GlowingInteractable: MonoBehaviour, IGvrPointerHoverHandler{

    public Material glowingMaterial;
    private Material normalMaterial;
    private new Renderer renderer;

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        
    }

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
        normalMaterial = renderer.material;
	}
	
    private void OnMouseEnter()
    {
        if (glowingMaterial != null)
        {
            renderer.material = glowingMaterial;
        }
    }

    private void OnMouseExit()
    {
        renderer.material = normalMaterial;
    }
}
