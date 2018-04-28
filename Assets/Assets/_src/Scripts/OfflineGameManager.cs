using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// When "X" button is clicked, switch to Home screen and exit VR mode.
/// </summary>
public class OfflineGameManager : MonoBehaviour
{

    public string HomeScene;
    // Use this for initialization
    void Update()
    {
#if !(UNITY_EDITOR)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(VRModeManager.SwitchTo2DMode());
            SceneManager.LoadScene(HomeScene ?? "HomeScene");
        }
#endif
    }
}
