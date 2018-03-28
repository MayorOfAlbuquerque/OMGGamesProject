﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ClueSpawner : MonoBehaviour {

    //list of characters in the game so corrrect clues can be chosen
    private List<CharacterSpec> charactersAlreadyInGame;
    private GameObject activeClueContainter;
    private Dictionary<CluePlaceholder, GameObject> clueReference;

	// Use this for initialization
	void Start() {
        clueReference = new Dictionary<CluePlaceholder, GameObject>();
        charactersAlreadyInGame = new List<CharacterSpec>();
        Debug.Log("-_____------------------Start-------------------______-");
        SetActiveContatiner();
        Debug.Log("Active Container _____________-----------------");
        SpawnGeneralClues();
        Debug.Log("Spawned Clues-------------------_____________________");
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
                Debug.Log("Clue spawned _________________________");
            }
        }
    }
    /*
    //spawn clues for char given and decide which text appears
    public void SpawnPrivateCluesForChar(CharacterSpec spec)
    {
        Debug.Log("spawning clues-----------------------");
        //if you == char then run spawn as private
        //check if private clues already place before adding

        if (!charactersAlreadyInGame.Contains(spec))
        { 
            //GameObject charCluesToSpawn = GameObject.Find("/PrivateClue1");
            Transform charCluesToSpawn = transform.GetChild(1);
            foreach (Transform child in charCluesToSpawn)
            {
                //TODO: if local player is spec then spawn private version
                SpawnClueInScene(child.GetComponent<CluePlaceholder>(), true);
                Debug.Log(spec.FullName);
            }
            charactersAlreadyInGame.Add(spec);
        }
        
    }
    */
    public void ChangeToPrivateText(CharacterSpec mySpec)
    {
        Debug.Log("Pos 1 +++++++++++++++_--------------------");
        foreach(KeyValuePair<CluePlaceholder, GameObject> entry in clueReference)
        {
            Debug.Log("Pos 1 +++++++++++++++_--------------------");
            //if a private clue and if you are the required recipient of each clue spec
            if (entry.Key.Clue.PrivateClue && entry.Key.Clue.Character.FullName == mySpec.FullName)
            {
                entry.Value.transform.GetChild(1).GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.PrivateDisplayText.ToString());
            }
            if (entry.Key.Clue.AltPrivateClue1 && entry.Key.Clue.AltCharacter1.FullName == mySpec.FullName)
            {
                entry.Value.transform.GetChild(1).GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.AltPrivateDisplayText1.ToString());
            }
            if (entry.Key.Clue.AltPrivateClue2 && entry.Key.Clue.AltCharacter2.FullName == mySpec.FullName)
            {
                entry.Value.transform.GetChild(1).GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.AltPrivateDisplayText2.ToString());
            }
            if (entry.Key.Clue.AltPrivateClue3 && entry.Key.Clue.AltCharacter3.FullName == mySpec.FullName)
            {
                entry.Value.transform.GetChild(1).GetComponent<TextOnHover>().ChangeText(entry.Key.Clue.AltPrivateDisplayText3.ToString());
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
                clue.gameObject.transform.GetChild(1).GetComponent<TextOnHover>().ChangeText(placeholder.Clue.GeneralDisplayText.ToString());
                clueReference.Add(placeholder, clue);
            }catch(Exception e) {
                Debug.LogWarning(e.Message);
                Debug.Log("We got an exception bois");
            }

        }
    }
   
}
