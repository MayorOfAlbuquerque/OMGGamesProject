using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ClueSpawner : NetworkBehaviour {

    //list of characters in the game so corrrect clues can be chosen
    private List<CharacterSpec> charactersAlreadyInGame;
    private GameObject activeClueContainter;
    private Dictionary<CluePlaceholder, GameObject> clueReference;
    //private Dictionary<GameObject, CluePlaceholder> reverseClueReference;
    CharacterSpec localSpec;

    // Use this for initialization
    void Start() {
        clueReference = new Dictionary<CluePlaceholder, GameObject>();
        charactersAlreadyInGame = new List<CharacterSpec>();
        SetActiveContatiner();
        SpawnGeneralClues();
	}

    private void SetActiveContatiner()
    {
        this.activeClueContainter = GameObject.Find("ActiveClueContainer");
        if(activeClueContainter == null)
        {
            Debug.Log("container is a null");
        }
    }

    //spawn all general clues
    public void SpawnGeneralClues()
    {
        Transform generalClues = transform.GetChild(0);
        //for all children of controller
        if (generalClues != null)
        {
            foreach (Transform child in generalClues)
            {
                SpawnClueInScene(child.GetComponent<CluePlaceholder>());
            }
        }
    }
    
    public void ChangeToPrivateText(CharacterSpec mySpec)
    {
        localSpec = mySpec;
        Debug.Log("-_-_-_-_-____________________"+localSpec);
        foreach(KeyValuePair<CluePlaceholder, GameObject> entry in clueReference)
        {
            try
            {
                //if a private clue and if you are the required recipient of each clue spec
                if (entry.Key.Clue.PrivateClue && entry.Key.Clue.Character.FullName == mySpec.FullName)
                {
                    entry.Value.GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.PrivateDisplayText.ToString());
                }
                if (entry.Key.Clue.AltPrivateClue1 && entry.Key.Clue.AltCharacter1.FullName == mySpec.FullName)
                {
                    entry.Value.GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.AltPrivateDisplayText1.ToString());
                }
                if (entry.Key.Clue.AltPrivateClue2 && entry.Key.Clue.AltCharacter2.FullName == mySpec.FullName)
                {
                    entry.Value.GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.AltPrivateDisplayText2.ToString());
                }
                if (entry.Key.Clue.AltPrivateClue3 && entry.Key.Clue.AltCharacter3.FullName == mySpec.FullName)
                {
                    entry.Value.GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.AltPrivateDisplayText3.ToString());
                }
            } catch(Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    //replace placeholder with real clue
    private void SpawnClueInScene(CluePlaceholder placeholder)
    {

        if (placeholder != null && placeholder.Clue && placeholder.Clue.ModelPrefab != null)
        {
            if (!placeholder.gameObject.activeInHierarchy)
            {
                return;
            }
            try
            {
                //instantiate a replacement object for the placehodler, place in active clue container
                placeholder.gameObject.SetActive(false);
                GameObject clue = Instantiate(
                    placeholder.Clue.ModelPrefab,
                    placeholder.transform.position,
                    placeholder.transform.rotation,
                    activeClueContainter.transform
                );
                //assign the hoverable text to what is said in the clue general text
                clue.GetComponent<TextOnHover>().ChangeText(placeholder.Clue.GeneralDisplayText.ToString());
                clueReference.Add(placeholder, clue);
            }
            catch(Exception e) {
                Debug.LogWarning(e.Message);
                Debug.Log("We got an exception bois");
            }

        }
    }

    public void RemoveClueServer(string spec)
    {
        RemoveClueModel(spec);
        RpcRemoveClue(spec);
    }
    
    public void RemoveClueModel(string spec)
    {
        CluePlaceholder p = GetPlaceholderFromSpecName(spec);
        //reverseClueReference.Remove(clue);
        GameObject clue;
        clueReference.TryGetValue(p, out clue);
        //clueReference.Remove(p);
        Destroy(clue);
        Debug.Log("Successful destroy");
    }

    [ClientRpc]
    public void RpcRemoveClue(string spec)
    {
        RemoveClueModel(spec);
    }

    public CluePlaceholder GetCluePlaceholderFromSpec(ClueSpec spec)
    {
        foreach (KeyValuePair<CluePlaceholder, GameObject> entry in clueReference)
        {
            if (entry.Key.Clue == spec)
            {
                return entry.Key;
            }
        }
        Debug.Log("-------- return null");
        return null;
    }


    public CluePlaceholder GetPlaceholderFromSpecName(string clueName)
    {
        foreach (KeyValuePair<CluePlaceholder, GameObject> entry in clueReference)
        {
            if (entry.Key.Clue.Name == clueName)
            {
                return entry.Key;
            }
        }
        return null;
    }

    public CluePlaceholder GetPlaceholderFromClue(GameObject clue)
    {
        foreach (KeyValuePair<CluePlaceholder, GameObject> entry in clueReference)
        {
            if (entry.Value == clue)
            {
                return entry.Key;
            }
        }
        return null;
    }

    public void ReplaceClue(string currentClueInScene, string newClue)
    {
        Debug.Log("replce enter");
        //get both placeholders
        CluePlaceholder currentPlaceholder = GetPlaceholderFromSpecName(currentClueInScene);
        CluePlaceholder newPlaceholder = GetPlaceholderFromSpecName(newClue);
        //remove old model
        RemoveClueModel(currentClueInScene);
        //place different clue
        if(newPlaceholder == null )
        {
            Debug.Log("Very bad");
        }
        if (currentPlaceholder == null)
        {
            Debug.Log("Very bad");
        }
        GameObject clue = Instantiate(
                   newPlaceholder.Clue.ModelPrefab,
                   currentPlaceholder.transform.position,
                   currentPlaceholder.transform.rotation,
                   activeClueContainter.transform
               );
        //set placeholder reference to new position
        newPlaceholder.transform.position = currentPlaceholder.transform.position;
        newPlaceholder.transform.rotation = currentPlaceholder.transform.rotation;
        clueReference[newPlaceholder] = clue;
        //assign general text
        clue.GetComponent<TextOnHover>().ChangeText(newPlaceholder.Clue.GeneralDisplayText.ToString());
        Debug.Log("Clue replaced");
    }

    public void ReplaceClueText()
    {
        Debug.Log("replacing text" + localSpec.ToString());
        ChangeToPrivateText(localSpec);
    }

}
