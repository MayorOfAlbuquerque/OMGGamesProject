using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClueSpawner : NetworkBehaviour {

    private GameObject activeClueContainter;
    private Dictionary<CluePlaceholder, GameObject> clueReference;
    //private Dictionary<GameObject, CluePlaceholder> reverseClueReference;
    CharacterSpec localSpec;
    GameObject beginningTextContainer;
    private float R = 0x85, G = 0x00, B = 0x00, A = 0x93;
    // Use this for initialization
    void Start() {
        clueReference = new Dictionary<CluePlaceholder, GameObject>();
        SetActiveContatiner();
        SpawnGeneralClues();
        localSpec = null;
        beginningTextContainer = GameObject.Find("BeginningText");
        //get local spec
        GetLocalSpec();
        //change to private text
        ChangeToPrivateText();
        //remove non-local player intro texts
        RemoveIntroTexts();
	}

    private void RemoveIntroTexts()
    {
        int i = 0;
        while (beginningTextContainer.transform.GetChild(i) != null)
        {
            Debug.Log("-------name = " + beginningTextContainer.transform.GetChild(i).name);
            if(beginningTextContainer.transform.GetChild(i).name == localSpec.FullName)
            {
                beginningTextContainer.transform.GetChild(i).gameObject.SetActive(true);
            }
            i++;
        }
        return;
    }

   
    private void SetActiveContatiner()
    {
        this.activeClueContainter = GameObject.Find("ActiveClueContainer");
        if(activeClueContainter == null)
        {
            Debug.Log("container is a null");
        }
    }
    //loop through players to find local one and assign spec
    private void GetLocalSpec()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length != 0)
        {
            foreach (GameObject player in players)
            {
                Debug.Log(player.ToString());
                if(player.GetComponent<Player>() !=null)
                {
                    CharacterSpec newSpec = player.GetComponent<Player>().GetSpecIfLocal();
                    if (newSpec != null)
                    {
                        this.localSpec = newSpec;
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Clue spawner: no players found");
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
    
    public void ChangeToPrivateText()
    {   
        foreach(KeyValuePair<CluePlaceholder, GameObject> entry in clueReference)
        {
            Boolean isPrivate = false; 
            try
            {
                //if a private clue and if you are the required recipient of each clue spec
                if (entry.Key.Clue.PrivateClue && entry.Key.Clue.Character.FullName == localSpec.FullName)
                {
                    entry.Value.GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.PrivateDisplayText.ToString());
                    isPrivate = true;
                }
                if (entry.Key.Clue.AltPrivateClue1 && entry.Key.Clue.AltCharacter1.FullName == localSpec.FullName)
                {
                    entry.Value.GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.AltPrivateDisplayText1.ToString());
                    isPrivate = true;
                }
                if (entry.Key.Clue.AltPrivateClue2 && entry.Key.Clue.AltCharacter2.FullName == localSpec.FullName)
                {
                    entry.Value.GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.AltPrivateDisplayText2.ToString());
                    isPrivate = true;
                }
                if (entry.Key.Clue.AltPrivateClue3 && entry.Key.Clue.AltCharacter3.FullName == localSpec.FullName)
                {
                    entry.Value.GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.AltPrivateDisplayText3.ToString());
                    isPrivate = true;
                }
                if (isPrivate)
                {
                    ColourPrivatePanel(entry.Value);
                }
            } catch(Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    private void ColourPrivatePanel(GameObject clueObject)
    {
        GameObject panel = clueObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        if (panel != null)
        {
            Color myColor = new Color(R, G, B);
            myColor.a = A;
            panel.GetComponent<Image>().color = myColor;
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
        ChangeToPrivateText();
    }

}
