using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class StoryScenesComposer : MonoBehaviour
{
    public Story story;
    public bool loadOnStart;
	// Use this for initialization
	void Start()
	{
        if(loadOnStart) {
            LoadStoryScenes(story);   
        }
	}

    public void LoadStoryScenes(Story story)
    {
        if (story == null || story.scenes == null)
        {
            Debug.Log("Null story ");
            return;
        }
        IEnumerable<string> scenes = story.scenes.Select(reference => reference.sceneName);
        StartCoroutine(SceneLoader.LoadAllScenes(scenes));
    }
}
