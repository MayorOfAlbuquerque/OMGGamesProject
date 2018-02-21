using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OverlayFader : MonoBehaviour {
    public float targetAlpha = 0.0f;
    public float startAlpha = 1.0f;
    private float startTime = 0.0f;
    public float duration = 0.5f;
    public Image image;
	// Use this for initialization
	void Start () {
        image.color = new Color(0, 0, 0, startAlpha);
	}
	
    public void FadeToClear() {
        startTime = Time.time;
        startAlpha = 1.0f;
        targetAlpha = 0.0f;
    }

    public void FadeToBlack() {
        startTime = Time.time;
        startAlpha = 0.0f;
        targetAlpha = 1.0f;
    }
	// Update is called once per frame
	void Update () {
        float t = (Time.time - startTime) / duration;
        float alpha = Mathf.SmoothStep(startAlpha, targetAlpha, t);
        image.color = new Color(0, 0, 0, alpha);
	}
}
