using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TimerSpy : NetworkBehaviour
{

    [SerializeField]
    public TimerControllerScript my_target;

    [SerializeField]
    public float currentSecondsLeftTimerOne;

    [SerializeField]
    public float currentSecondsLeftTimerTwo;

    [SerializeField]
    public Text firstTimerText;

    [SerializeField]
    public Text secondTimerText;

    [SerializeField]
    public Text inputText;

    [SerializeField]
    public Text timerSet;

    bool haveConfirmed = false;

    // Use this for initialization
    void Start()
    {
        firstTimerText.text = "First Timer";
        secondTimerText.text = "Second Timer";
        if (my_target == null)
        {
            Debug.Log("TimerSpy has not found it's target. Drag and drop the correct prefab");
        }
        currentSecondsLeftTimerOne = 30;
    }

    // Update is called once per frame
    void Update()
    {
        GetTimerUpdate();

        firstTimerText.text = currentSecondsLeftTimerOne.ToString();
        secondTimerText.text = currentSecondsLeftTimerTwo.ToString();
    }

    void GetTimerUpdate()
    {
        currentSecondsLeftTimerOne = my_target.GetFirstTimer();
        if (currentSecondsLeftTimerOne <= 0)
        {
            currentSecondsLeftTimerTwo = my_target.GetSecondTimer();
        }

    }

    public void TeleportToVoting()
    {
        if (inputText.text == "Potato" || inputText.text == "potato" || inputText.text == "POTATO")
        {
            my_target.BroadcastEndTimer();
            my_target.BroadcastEndTimerSecond();
        }

    }

    public void ToggleServerTimer()
    {
        my_target.ServerForceStop();
    }

    public void ResetTimers()
    {
        my_target.ResetTimers();
    }

    public void SetTimerOne()
    {
        bool isNumeric = timerSet.text.All(char.IsDigit);
        if (isNumeric)
        {
            int this_time = Convert.ToInt32(timerSet.text);
            my_target.SetFirstTimer(this_time);
        }
    }

    public void SetTimerTwo()
    {
        bool isNumeric = timerSet.text.All(char.IsDigit);
        if (isNumeric)
        {
            int this_time = Convert.ToInt32(timerSet.text);
            my_target.SetSecondTimer(this_time);
        }
       
    }
}
