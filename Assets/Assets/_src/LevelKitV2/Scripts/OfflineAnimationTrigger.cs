using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

public class OfflineAnimationTrigger : MonoBehaviour
{
    public PlayableDirector director;

	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "Player") {
            PlayAnimation();        
        }
	}

    public void PlayAnimation()
    {
        if (director == null)
        {
            Debug.LogWarning("playable director is null");
            return;
        }
        director.Play();
    }
}
