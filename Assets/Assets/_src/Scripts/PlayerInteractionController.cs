using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


/// <summary>
/// Class that handles any player interations that should be networked, 
/// particularly animations that need to be shown for every other player in the game.
/// </summary>
public class PlayerInteractionController : NetworkBehaviour{
	
	private GameObject thisGameObject; //The object this script is attached to, used to access the "player".

	// Use this for initialization
	void Start () {
		thisGameObject = this.gameObject;
	}

	/// <summary>
	/// Method that handles player interaction with an interactable gameobject
	/// </summary>
	/// <param name="obj">The game object the player is trying to interact with</param> 
    public void HandleOnClickAction (GameObject obj, ref RaycastResult result)
	{
		if(Input.anyKeyDown && !Input.GetButton("Horizontal") && !Input.GetButton("Vertical")) //LMB release
		   {
			
            if(obj?.GetComponent<Attackable>())
            {
                CmdAttackPlayer(obj);
            }
            
		    else if (obj?.GetComponent<PickupController>()) {
			    CmdPlayerLeftClickWeapon(obj, thisGameObject); //Weapon pickup needs to know about the player gameobject
                Debug.Log("Left clicked Weapon \n");
            } 
		    else {
			    CmdPlayerLeftClick (obj);
                Debug.Log("Left clicked Object! \n");
		    }
			
        }
		//Extend this as necessary
	}

    public void HandleOnHoverEnterAction(GameObject obj, ref RaycastResult result) {
        InteractableObjectController controller = obj.GetComponent<InteractableObjectController>();
        if (controller == null)
        {
            return;
        }
        string helpText = controller.HelpText ?? "";

        ExecuteEvents.Execute<IHelpTextDisplay>(
            thisGameObject,
            null,
            (x, y) =>
            {
                x.Show(helpText);
            });
        controller.OnHoverEnter();
    }

    public void HandleOnHoverExitAction(GameObject obj, ref RaycastResult result) {
        InteractableObjectController controller = obj.GetComponent<InteractableObjectController>();
        if (controller == null)
        {
            return;
        }
        ExecuteEvents.Execute<IHelpTextDisplay>(
            thisGameObject,
            null,
            (x, y) =>
            {
                x.Hide();
            });
        controller.OnHoverExit();
    }
    /// <summary>
    /// Runs on the server, allowing the calling of Rpc's to display animation to all clients. 
    /// </summary>
    /// <param name="obj">The game object the player is trying to interact with</param> 
    [Command]
    public void CmdPlayerLeftClick(GameObject obj) //Cant have overloaded commands, so need to use optional arguments instead
    {
        InteractableObjectController controller = obj.GetComponent<InteractableObjectController>();
        if (controller == null)
        {
            return;
        }
        controller.OnClick (); //Needs to run on server as players do not have authority over interactable objects
	}

    [Command]
    public void CmdAttackPlayer(GameObject obj) 
    {
        Debug.Log("CMDATTACK");
        //obj.GetComponent<Attackable>().OnClick();
		obj.GetComponent<InteractableObjectController>().OnClick(thisGameObject.GetComponent<Player>()); //Added the attacking player as argument to Onclick here.
    }

    /// <summary>
    /// Runs on the server, allowing the calling of Rpc's to display animation to all clients. 
    /// </summary>
    /// <param name="obj">The game object the player is trying to interact with</param> 
    [Command]
	public void CmdPlayerLeftClickWeapon (GameObject obj, GameObject thisPlayer)
	{
		obj.GetComponent<InteractableObjectController> ().OnClick (thisPlayer.GetComponent<Player>()); //Needs to run on server as players do not have authority over interactable objects

	}

	/// <summary>
	/// Runs on the server, allowing the calling of Rpc's to display animation to all clients. 
	/// </summary>
	/// <param name="obj">The game object the player is trying to interact with</param> 
	[Command]
	void CmdPlayerRightClick (GameObject obj)
	{

	}
}
