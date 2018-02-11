using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Base class for all player interactable objects.
/// </summary>
public abstract class InteractableObjectController : NetworkBehaviour, IControllerInteractable
{
    public string HelpText;
    public abstract void OnClick();
    public abstract void OnKeyDown(KeyCode code);
    public abstract void OnKeyUp(KeyCode code);
	public virtual void OnClick (object obj)
	{
		
	}
}
