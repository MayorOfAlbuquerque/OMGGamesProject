using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClueSpawner : MonoBehaviour {

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

    public void SpawnClues(Scene scene) {
        GameObject[] rootObjects = scene.GetRootGameObjects();

        CluePlaceholder[] placeholders;
        foreach(GameObject obj in rootObjects) {
            placeholders = obj.GetComponentsInChildren<CluePlaceholder>();
            foreach (CluePlaceholder placeholder in placeholders)
                SpawnClueInScene(placeholder);    
        }
    }

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
