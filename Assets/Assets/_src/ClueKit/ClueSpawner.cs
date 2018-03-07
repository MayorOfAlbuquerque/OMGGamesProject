﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ClueSpawner : MonoBehaviour {

    //list of characters in the game so corrrect clues can be chosen
    private List<CharacterSpec> charactersAlreadyInGame;
    private GameObject activeClueContainter;

	// Use this for initialization
	void Start() {
        SetActiveContatiner();
        SpawnCluesForCurrentScene();
	}

    private void SetActiveContatiner()
    {
        this.activeClueContainter = GameObject.Find("ActiveClueContainer");
    }

    public void SpawnCluesForCurrentScene() {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene != null)
        {
            SpawnGeneralClues(activeScene);
        }
    }

    //spawn all general clues
    public void SpawnGeneralClues(Scene scene)
    {
        Transform generalClues = transform.GetChild(0);
        //for all children of controller
        foreach (Transform child in generalClues)
        {
            SpawnClueInScene(child.GetComponent<CluePlaceholder>(), false);  
        }

    }

    //spawn clues for char given and decide which text appears
    public void SpawnPrivateCluesForChar(List<CharacterSpec> characters)
    {
        //if you == char then run spawn as private
        //check if private clues already place before adding
        foreach(CharacterSpec spec in characters)
        {
            if(!charactersAlreadyInGame.Contains(spec))
            {
                //GameObject charCluesToSpawn = GameObject.Find("/PrivateClue1");
                GameObject charCluesToSpawn = transform.GetChild(1).gameObject;
                foreach (Transform child in charCluesToSpawn.transform)
                {
                    SpawnClueInScene(child.GetComponent<CluePlaceholder>(), false);
                }
            }
        }
    }

    //replace placeholder with real clue
    private void SpawnClueInScene(CluePlaceholder placeholder, bool isPrivate)
    {
        if (placeholder != null && placeholder.Clue && placeholder.Clue.ModelPrefab != null)
        {
            if (!placeholder.gameObject.activeInHierarchy)
            {
                return;
            }
            //instantiate a replacement object for the placehodler, place in active clue container
            placeholder.gameObject.SetActive(false);
            GameObject clue = Instantiate(
                placeholder.Clue.ModelPrefab,
                placeholder.transform.position,
                placeholder.transform.rotation,
                activeClueContainter.transform.GetChild(0).transform
            );

            //assign the hoverable text to what is said in the clue general text
            if (!isPrivate)
            {
                clue.GetComponent<TextOnHover>().ChangeText(placeholder.Clue.GeneralDisplayText.ToString());
            }
        }
    }
   /* [ClientRpc]
    public void RpcSpawnPrivateClues(List<CharacterSpec> charList)
    {
        //Call all client's clue spawners with list of current players
        GameObject clueController = GameObject.Find("ClueController");
        clueController.GetComponent<ClueSpawner>().SpawnPrivateCluesForChar(charList);
    }*/
}
