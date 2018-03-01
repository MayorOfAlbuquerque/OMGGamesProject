using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClueSpawner : MonoBehaviour {

    //list of characters in the game so corrrect clues can be chosen
    private CharacterSpec[] charactersInGame;
    private GameObject activeClueContainter;

	// Use this for initialization
	void Start () {
        SetActiveContatiner();
        SpawnGeneralCluesForCurrentScene();
	}

    private void SetActiveContatiner()
    {
        this.activeClueContainter = GameObject.Find("ActiveClueContainer");
    }

    public void SpawnGeneralCluesForCurrentScene() {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene != null)
        {
            SpawnClues(activeScene);
        }
    }

    private void SpawnPrivateCharacterClues()
    {

    }

    public void SpawnClues(Scene scene)
    {
        /*GameObject[] rootObjects = scene.GetRootGameObjects();

        //loop through all root objects in scene to find placeholders
        CluePlaceholder[] placeholders;
        foreach(GameObject obj in rootObjects) {
            placeholders = obj.GetComponentsInChildren<CluePlaceholder>();
            foreach (CluePlaceholder placeholder in placeholders)
                SpawnClueInScene(placeholder);    
        }*/

        //check which private clues to spawn
        //spawn general clues
        //spawn private clues

        //for all children of controller
        foreach (Transform child in transform)
        {
            if (!child.GetComponent<CluePlaceholder>().IsCluePrivate())
            {
                SpawnClueInScene(child.GetComponent<CluePlaceholder>());   
            }
        }

    }

    //replace placeholder with real clue
    private void SpawnClueInScene(CluePlaceholder placeholder) {
        if(placeholder != null && placeholder.Clue && placeholder.Clue.ModelPrefab != null)
        {
            if(!placeholder.gameObject.activeInHierarchy) {
                return;
            }
            //instantiate a replacement object for the placehodler
            placeholder.gameObject.SetActive(false);
            GameObject clue =  Instantiate(
                placeholder.Clue.ModelPrefab,
                placeholder.transform.position,
                placeholder.transform.rotation,
                activeClueContainter.transform.GetChild(0).transform
            );

            //assign the hoverable text to what is said in the clue general text
            clue.GetComponent<TextOnHover>().ChangeText(placeholder.Clue.GeneralDisplayText.ToString());
        }
    }


}
