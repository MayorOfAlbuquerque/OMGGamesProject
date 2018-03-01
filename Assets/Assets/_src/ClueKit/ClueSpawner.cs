using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClueSpawner : MonoBehaviour {

    //list of characters in the game so corrrect clues can be chosen
    private CharacterSpec[] charactersInGame;

	// Use this for initialization
	void Start () {
        SpawnCluesForCurrentScene();
	}

    public void SpawnCluesForCurrentScene() {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene != null)
        {
            SpawnClues(activeScene);
        }
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
            placeholder.gameObject.SetActive(false);
            Instantiate(
                placeholder.Clue.ModelPrefab,
                placeholder.transform.position,
                placeholder.transform.rotation
            );    
        }
    }
}
