using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


/// <summary>
/// Class that handles any player interations that should be networked, 
/// particularly animations that need to be shown for every other player in the game.
/// </summary>
public class PlayerInteractionController : NetworkBehaviour {

    private GameObject thisGameObject; //The object this script is attached to, used to access the "player".

    // Use this for initialization
    void Start() {
        thisGameObject = this.gameObject;
    }

    /// <summary>
    /// Method that handles player interaction with an interactable gameobject
    /// </summary>
    /// <param name="obj">The game object the player is trying to interact with</param> 
    public void HandleOnClickAction(GameObject obj, ref RaycastResult result)
    {
        if (Input.anyKeyDown && !Input.GetButton("Horizontal") && !Input.GetButton("Vertical")) //LMB release
        {
            CluePickupController controller = obj.GetComponent<CluePickupController>() ?? obj.GetComponentInParent<CluePickupController>();
            Debug.Log(obj.name);
            Debug.Log(result);
            Debug.Log(controller.ToString());
            if (controller != null) {
                Debug.Log("pos1");
                Debug.Log(controller.ToString());
                CluePlaceholder spec = controller.spec;
                Debug.Log(spec.ToString() + "-------------------------");
                CmdPlayerLeftClickClue(spec.Clue.Name.ToString(), thisGameObject);
                Debug.Log("Left clicked Clue \n");
            }
            else {
                CmdPlayerLeftClick(obj);
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
        controller.OnClick(); //Needs to run on server as players do not have authority over interactable objects
    }

    /// <summary>
    /// Runs on the server, allowing the calling of Rpc's to display animation to all clients. 
    /// </summary>
    /// <param name="obj">The game object the player is trying to interact with</param> 
    [Command]
    public void CmdPlayerLeftClickClue(string specName, GameObject thisPlayer)
    {
        //check for if murderer
        if (thisPlayer.GetComponent<Player>().IsMurderer())
        {
            Debug.Log("------Player is murderer");
            GameObject clueSpawner = GameObject.Find("ClueController");
            CluePlaceholder spec = clueSpawner.GetComponent<ClueSpawner>().GetPlaceholderFromSpecName(specName);
            //check if player holds clue
            string currentClue = thisPlayer.GetComponent<Player>().GetHeldClue();
            //rpc remove model or swap
            if (currentClue != null)
            {
                //current held clue = currentClue
                //got specname
                //clue to hold = spec name
                thisPlayer.GetComponent<Player>().SetHeldClue(specName);
                thisPlayer.GetComponent<Player>().RpcSetClue(specName);
                //remove clue model
                //place new clue model with text
                clueSpawner.GetComponent<ClueSpawner>().ReplaceClue(specName, currentClue);
                RpcReplaceClue(specName, currentClue);
                RpcReplaceText();
            }
            else
            {
                //rpc set player holding clue
                Debug.Log("Adding new clue to murderer");
                thisPlayer.GetComponent<Player>().SetHeldClue(specName);
                thisPlayer.GetComponent<Player>().RpcSetClue(specName);
                //remove models on server and clients
                clueSpawner.GetComponent<ClueSpawner>().RemoveClueModel(specName);
                RpcRemoveClue(specName);
            }
        }
    }

    [ClientRpc]
    public void RpcRemoveClue(string spec)
    {
        GameObject clueSpawner = GameObject.Find("ClueController");
        clueSpawner.GetComponent<ClueSpawner>().RemoveClueModel(spec);
    }

    [ClientRpc]
    public void RpcReplaceClue(string oldClue, string newClue)
    {
        GameObject clueSpawner = GameObject.Find("ClueController");
        clueSpawner.GetComponent<ClueSpawner>().ReplaceClue(oldClue, newClue);
    }

    [ClientRpc]
    public void RpcReplaceText()
    {
        GameObject clueSpawner = GameObject.Find("ClueController");
        clueSpawner.GetComponent<ClueSpawner>().ReplaceClueText();
    }
}
