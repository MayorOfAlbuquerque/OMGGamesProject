using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineGameManager : MonoBehaviour
{

    public GameObject ServerOnly;
    // Use this for initialization
    void Start()
    {
        // turn off gvr emulator when running the game in editor or on desktop,
        // need this to spectator cam to work.
#if UNITY_EDITOR || UNITY_STANDALONE
        GameObject.Find("GvrEditorEmulator")?.SetActive(false);
        GameObject.Find("GvrHeadset")?.SetActive(false);
        GameObject.Find("GvrControllerMain")?.SetActive(false);
        Camera.main.gameObject.isStatic = false;

        if (NetworkManager.singleton == null
           || NetworkManager.singleton.IsClientConnected())
        {
            ServerOnly?.SetActive(false);
        }
#endif
    }
    // Update is called once per frame
    void Update()
    {
#if !(UNITY_EDITOR)
        if(NetworkManager.singleton == null) {
            return;
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) {
            if(NetworkManager.singleton.IsClientConnected()){
                NetworkManager.singleton.StopClient();
                StartCoroutine(VRModeManager.SwitchTo2DMode());
            }
        }
#endif
	}

}
