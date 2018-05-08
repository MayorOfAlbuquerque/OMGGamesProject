using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class SceneLoadTrigger : MonoBehaviour {
    private bool loaded;

    [SerializeField]
    private string sceneName;
    private AsyncOperation operation;

	private void OnTriggerEnter(Collider other)
	{
        if(string.IsNullOrWhiteSpace(sceneName) || loaded || operation != null) {
            return;
        }
        StartCoroutine(LoadScene());
        
	}

    public IEnumerator LoadScene() {
        operation = SceneManager
            .LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while(!operation.isDone) {
            yield return new WaitForSeconds(0.1f);
        }
        operation = null;
        loaded = true;
        yield return true;
    }
}
