﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeaconPingScript : MonoBehaviour
{
    AndroidJavaClass jc;
    string javaMessage = "";
    [SerializeField] Text textBox;

    void Start()
    {
        // Acces the android java receiver we made
        jc = new AndroidJavaClass("com.example.omg.myapplication.UnityReciever");
        // We call our java class function to create our MyReceiver java object
        jc.CallStatic("createInstance");
    }

    void Update()
    {
        // We get the text property of our receiver
        javaMessage = jc.GetStatic<string>("intentMessage");
        textBox.text = javaMessage;
    }
}