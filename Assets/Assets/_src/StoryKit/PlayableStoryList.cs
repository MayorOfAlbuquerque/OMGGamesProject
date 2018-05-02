using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "StoryList", menuName = "MurderMystery/Playable Stories", order = 7)]
public class PlayableStoryList : ScriptableObject
{
    public List<Story> stories;


    public Story GetStoryById(int id)
    {
        Story result = null;
        foreach (var story in stories)
        {
            if (story.StoryId == id)
            {
                result = story;
                break;
            }
        }
        return result;
    }
}
