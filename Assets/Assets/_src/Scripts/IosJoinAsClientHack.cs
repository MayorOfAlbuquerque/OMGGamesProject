using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Currently bluetooth controller does not work with iPhone, Use this script
/// to join a game as client. Just double tap on the screen.
/// </summary>
public class IosJoinAsClientHack : MonoBehaviour
{
    public float doubleTapInterval;
    private int tapCount = 0;
    private float nextTapTime = 0.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_IOS
        if(Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended) {
                tapCount++;
            }

            if(tapCount == 1) {
                nextTapTime = Time.time + doubleTapInterval;
            }
            else if(tapCount == 2 && Time.time <= nextTapTime) {
                //JoinAsClient.Join();
                JoinAsHost.Host();
                tapCount = 0;
                nextTapTime = 0.0f;
            }
            else {
                tapCount = 0;
                nextTapTime = 0.0f;
            }
        }
#endif
    }
}
