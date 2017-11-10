using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTrigger : MonoBehaviour {

    [SerializeField] string levelToLoad;

    private void OnTriggerEnter(Collider other) {

        Debug.Log("adf");
        if(other.gameObject.tag == "Player") {
            SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
        }
    }
}
