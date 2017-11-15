using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all player interactable objects.
/// </summary>
public abstract class InteractableObjectController : MonoBehaviour, IControllerInteractable
{
    public abstract void OnClick();
}
