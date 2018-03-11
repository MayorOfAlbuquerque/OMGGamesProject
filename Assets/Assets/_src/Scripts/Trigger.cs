using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTrigger : MonoBehaviour {

    [SerializeField] string sceneToLoad;
    [SerializeField] 
	public GameObject Beacon2;

    //triggers when another object enters its area.
    private void OnTriggerEnter(Collider other) {

        Debug.Log("so triggered right now");
        //if object is player then load scene
        if(other.gameObject.tag == "Player") {
            this.gameObject.transform.position = Beacon2.transform.position;
        }
    }
}
