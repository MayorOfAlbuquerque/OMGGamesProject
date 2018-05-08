
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class JoinAsHost : MonoBehaviour
{
    public OverlayFader fader;
    //triggers when another object enters its area.
    private void OnTriggerEnter(Collider other)
    {
        if(NetworkManager.singleton == null) {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            fader.duration = 0.8f;
            StartCoroutine(fader.FadeToBlackAndDo(Host()));
        }
    }

    public IEnumerator Host() {
        NetworkManager.singleton.StartHost(); //Starts as host, NetworkManger loads online scene
        yield return new WaitForEndOfFrame();
    }
}
