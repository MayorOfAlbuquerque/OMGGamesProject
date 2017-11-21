using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoiceChat;

public class PlayerVoiceChat : MonoBehaviour {

    

	// Use this for initialization
	void Start () {

        Application.RequestUserAuthorization(UserAuthorization.Microphone);
        foreach (string device in VoiceChatRecorder.Instance.AvailableDevices)
        {
            Debug.Log(device);
            VoiceChatRecorder.Instance.Device = device; // Setting the device being used
        }

        
    }
	
	// Update is called once per frame
	void Update () {

        if (VoiceChatRecorder.Instance.Device == null)
        {
            Debug.Log("Everyone Panic. No audio recorder!");
        }
        else
        {
            if (Input.GetKeyDown(VoiceChatRecorder.Instance.PushToTalkKey)) // Down = Start Recording
            {
                Debug.Log("GetKeyDown returned True");
                if (VoiceChatRecorder.Instance.IsRecording == false)
                {
                    VoiceChatRecorder.Instance.StartRecording();
                }
                else { Debug.Log("is Recording"); }
            }

            if (Input.GetKeyUp(VoiceChatRecorder.Instance.PushToTalkKey)) // Up = Stop recording
            {
                Debug.Log("GetKeyUp returned True");
                if (VoiceChatRecorder.Instance.IsRecording)
                {
                    VoiceChatRecorder.Instance.StopRecording();
                }
                else { Debug.Log("Was not recording before trying to stop recording"); }
            }
            else
            {
                Debug.Log("Neither GetKeyUp or GetKeyDown returned True");
            }
        }
        
    }
}
