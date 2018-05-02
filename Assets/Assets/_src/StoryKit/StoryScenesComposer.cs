using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StoryScenesComposer : MonoBehaviour
{
    public Story defaultStory;
    public Story customStory;
    public bool loadOnStart;
	// Use this for initialization
	void Start()
	{
        
        // non networked
        if(loadOnStart && NetworkManager.singleton == null){
            LoadStoryScenes(defaultStory);
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

    public void LoadCustomStoryScenes() {
        if (customStory != null)
        {
            LoadStoryScenes(customStory);
        }
    }
}
