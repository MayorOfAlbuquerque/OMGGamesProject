using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class StoryServerManager : MonoBehaviour
{
    private Story story;
    public PlayableStoryList playableStories;
    public bool testingMode;
    public Story testingStory;
    public Story CurrentStory
    {
        get
        {
            return story;
        }
    }

	// Use this for initialization
	void Start()
	{
        RegisterHandlers();
	}
    void RegisterHandlers() {
        NetworkServer.RegisterHandler(GetStoryMessage.MESSAGE_TYPE, OnGetMessage);
    }
    void OnGetMessage(NetworkMessage message) {
        Debug.Log(message);
        if(message.ReadMessage<GetStoryMessage>() !=null) {
            var reply = new ReplyStoryMessage(story?.StoryId ?? -1);
            NetworkServer.SendToClient(
                message.conn.connectionId,
                ReplyStoryMessage.MESSAGE_TYPE,
                reply
            );
        }
    }
    public void ChooseStory() {
        if(testingMode) {
            story = testingStory;
            return;
        }
        if(playableStories == null || playableStories.stories == null) {
            return;
        }
        int selectedStoryIndex = (int) UnityEngine.Random.Range(0, playableStories.stories.Count);
        story = playableStories.stories[selectedStoryIndex];
    }
}
