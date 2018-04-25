using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneComposer : MonoBehaviour {

    [HideInInspector]
    public List<SceneReference> scenes = new List<SceneReference>();
    [SerializeField]
    public bool loadOnStart;
    [SerializeField]
    public Scene scene;
	// Use this for initialization
	void Start () {
        if (loadOnStart)
        {
            StartCoroutine(LoadAllScenes());
        }
	}

    IEnumerator LoadScene(string sceneName) {
        Debug.Log("loading scene: " + sceneName);
        if(string.IsNullOrEmpty(sceneName) || string.IsNullOrWhiteSpace(sceneName)){
            yield return null;
        }
        AsyncOperation operation = null;
        try
        {
            operation = SceneManager
                .LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        } catch(Exception e){
            Debug.LogError(e);
        }
        if (operation != null)
        {
            while (!operation.isDone)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            yield return null;
        }
    }
    IEnumerator LoadAllScenes() {
        foreach(SceneReference reference in scenes) {
            yield return LoadScene(reference.sceneName);
        }
    }
}
