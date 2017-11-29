using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoiceChat;
using UnityEngine.Networking;

public class PlayerVoiceChat : MonoBehaviour {

   
	// Use this for initialization
	void Start(){
        Debug.Log("Instance may exist");
        Debug.Log(VoiceChatRecorder.Instance);
        if(VoiceChatRecorder.Instance == null)
        {
            Debug.Log("##### No Instance Found #########");
        }
        Application.RequestUserAuthorization(UserAuthorization.Microphone);
        foreach (string device in VoiceChatRecorder.Instance.AvailableDevices)
        {
            Debug.Log(device);
            VoiceChatRecorder.Instance.Device = device; // Setting the device being used
        }
    }


    /**
     *  Some notes on trying to set up networkID for the instances
     *  Maybe extract it from the NetworkIdentity class.
     *  Documentation on Unity says that this is assigned by the server / network manager.
     *  could be a good way to extract and set networkIds.
     *  
     * **/
	
	// Update is called once per frame
	void Update () {
        if(VoiceChatRecorder.Instance == null)
        {
            Debug.Log("Instance is null");
        }
        if (VoiceChatRecorder.Instance.Device == null)
        {
            Debug.Log("Everyone Panic. No audio recorder!");
        }
        if(VoiceChatRecorder.Instance.NetworkId == 0)
        {
            Debug.Log("NetworkId not set");
        }
        else// Setup is correct
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
            if (VoiceChatRecorder.Instance.AutoDetectSpeech && VoiceChatRecorder.Instance.IsRecording == false)
            {
                VoiceChatRecorder.Instance.StartRecording();
            }
            else
            {
                if (VoiceChatRecorder.Instance.IsTransmitting)
                {
                    Debug.Log("Transmitting to somewhere");
                }
                Debug.Log("Neither GetKeyUp or GetKeyDown returned True");
            }
        }
        
    }
}
