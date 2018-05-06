using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Networking;

[Serializable]
public class StoryLoadEvent : UnityEvent<Story>
{
}
public class StoryClientManager : MonoBehaviour
{
    public PlayableStoryList playableStories;
    private NetworkClient client;
    private Story _currentStory;
    public StoryLoadEvent OnLoadStory;

    public Story CurrentStory
    {
        get
        {
            return _currentStory;
        }
    }
	// Use this for initialization
	void Start()
	{
        client = NetworkManager.singleton?.client;
        if(OnLoadStory == null) {
            OnLoadStory = new StoryLoadEvent();
        }
        RegisterHandlers();
        if(NetworkServer.active) {
            OMGNetManager manager = (NetworkManager.singleton as OMGNetManager);
            _currentStory = manager?.storyServerManager?.CurrentStory;
            StartCoroutine(NotifyEventListenersAsync());
        } else if(NetworkClient.active) {
            GetStory();
        }
	}

    void RegisterHandlers(){
        if(client == null) {
            return;
        }

        client.RegisterHandler(ReplyStoryMessage.MESSAGE_TYPE, OnReplyStory);

    }
    public void GetStory() {
        GetStoryMessage message = new GetStoryMessage();
        client?.Send(GetStoryMessage.MESSAGE_TYPE, message);
    }
    void OnReplyStory(NetworkMessage message) {
        var reply = message.ReadMessage<ReplyStoryMessage>();
        if(reply == null) {
            Debug.LogWarning("received reply story message but cannot be deserialized");
        }
        if(reply.storyId >= 0){
            _currentStory = playableStories.GetStoryById(reply.storyId);
            NotifyEventListeners();
        }
    }
    void NotifyEventListeners() {
        OnLoadStory?.Invoke(_currentStory);
    }

    IEnumerator NotifyEventListenersAsync() {
        yield return new WaitForSeconds(0.5f);
        NotifyEventListeners();
    }
}
