using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoiceChat : MonoBehaviour {



	// Use this for initialization
	void Start () {
        
        Application.RequestUserAuthorization(UserAuthorization.Microphone);
        
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
