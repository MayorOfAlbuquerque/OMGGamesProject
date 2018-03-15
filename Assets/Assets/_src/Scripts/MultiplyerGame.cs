using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MultiplyerGameSceneLoader : MonoBehaviour {
    public List<string> MainSceneParts;

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadAllScenes());
	}

    IEnumerator LoadScene(string sceneName) {
        AsyncOperation operation = SceneManager
            .LoadSceneAsync(sceneName);
        while(!operation.isDone) {
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator LoadAllScenes() {
        foreach(string sceneName in MainSceneParts) {
            yield return LoadScene(sceneName);
        }
    }
}
