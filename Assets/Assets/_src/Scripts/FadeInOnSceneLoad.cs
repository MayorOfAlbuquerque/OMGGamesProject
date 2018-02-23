using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOnSceneLoad : MonoBehaviour {

    public OverlayFader fader;
	// Use this for initialization
	void Start () {
        fader?.FadeToClear();
	}
}
