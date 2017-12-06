using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// When attached to an GameObject, it will switch to a different material when
/// the player is looking at it. It is used to get the glowing effect.
/// </summary>
[RequireComponent(typeof(Renderer))]
public class GlowingInteractable: MonoBehaviour {

    /// <summary>
    /// Material to use when glowing.
    /// </summary>
    public Material glowingMaterial;
    private Material normalMaterial;
    private new Renderer renderer;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
        normalMaterial = renderer.material;
	}

    public void SetGlowing() {
        if (glowingMaterial != null)
        {
            renderer.material = glowingMaterial;
        }
    }

    public void SetNormal() {
        renderer.material = normalMaterial;
    }
}
