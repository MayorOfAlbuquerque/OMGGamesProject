using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class OverlayFader : MonoBehaviour
{
    public float targetAlpha = 0.0f;
    public float startAlpha = 1.0f;
    private float startTime = 0.0f;
    public float duration = 0.5f;
    public Image image;
    public bool attachToMainCamera = true;
    public Color targetColor;

    private Canvas canvas;
    // Use this for initialization
    void Start()
    {
        image.color = new Color(0, 0, 0, startAlpha);
        canvas = GetComponent<Canvas>();
        if (attachToMainCamera)
        {
            canvas.worldCamera = Camera.main;
        }
        targetColor = new Color(0, 0, 0, startAlpha);
    }

    public void FadeToClear()
    {
        startTime = Time.time;
        startAlpha = 1.0f;
        image.color = new Color(0, 0, 0, startAlpha);
        targetAlpha = 0.0f;
    }

    public void FadeToBlack()
    {
        startTime = Time.time;
        startAlpha = 0.0f;
        image.color = new Color(0, 0, 0, startAlpha);
        targetAlpha = 1.0f;
    }

    public void FadeToRed()
    {
        startTime = Time.time;
        startAlpha = 0.0f;
        targetColor = new Color(255, 0, 0, startAlpha);
        targetAlpha = 1.0f;
        duration = 5.0f;
    }

    public void FadeToGreen()
    {
        startTime = Time.time;
        startAlpha = 0.0f;
        targetColor = new Color(0, 128, 0, startAlpha);
        targetAlpha = 1.0f;
        duration = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Time.time - startTime) / duration;
        float alpha = Mathf.SmoothStep(startAlpha, targetAlpha, t);
        image.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
    }

    public IEnumerator FadeToClearAndDo(IEnumerator next)
    {
        FadeToClear();
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(next);
    }

    public IEnumerator FadeToBlackAndDo(IEnumerator next)
    {
        FadeToBlack();
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(next);
    }
}