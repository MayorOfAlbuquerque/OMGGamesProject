using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AnimationTrigger : MonoBehaviour {
    public PlayableDirector director;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("playing animation");
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player") {
            PlayAnimation();
        }
    }

    public void PlayAnimation() {
        if(director == null) {
            Debug.LogWarning("playable director is null");
            return;
        }
        director.Play();
    }
}
