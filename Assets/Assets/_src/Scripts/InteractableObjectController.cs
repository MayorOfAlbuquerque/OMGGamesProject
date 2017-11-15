using UnityEngine;
using System.Collections;

public abstract class InteractableObjectController : MonoBehaviour, IControllerInteractable
{
    public abstract void OnClick();
}
