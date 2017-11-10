using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTrigger : MonoBehaviour {

    [SerializeField] string levelToLoad;

    //triggers when another object enters its area.
    private void OnTriggerEnter(Collider other) {

        Debug.Log("adf");
        //if object is player then load scene
        if(other.gameObject.tag == "Player") {
            SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
        }
    }
}
