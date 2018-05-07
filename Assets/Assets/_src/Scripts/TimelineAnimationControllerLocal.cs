using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayableDirector))]
public class TimelineAnimationControllerLocal: MonoBehaviour
{
    PlayableDirector pd;


    void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

	void Update()
	{
		if (Input.anyKeyDown && !Input.GetButton ("Horizontal") && !Input.GetButton ("Vertical")) {
			RaycastResult result = GvrPointerInputModule.CurrentRaycastResult;
			if (result.gameObject != null)
			{
				if (result.gameObject.GetComponent<TimelineAnimationControllerLocal>() != null)
				{
					result.gameObject.GetComponent<TimelineAnimationControllerLocal>().Open ();
				}
			}
		}
	}

    public void Close()
    {
    }


    public void Open()
    {
		Debug.Log ("Clicked the bar door");
        pd?.Play();
    }

}
