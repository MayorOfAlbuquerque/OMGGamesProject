﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class VRModeManager : MonoBehaviour {

    public string deviceName;
	// Use this for initialization
	void Start () {
        StartCoroutine(SwitchToVRMode());
	}

    public IEnumerator SwitchToVRMode() {
        string device = string.IsNullOrEmpty(deviceName) 
                              ? "cardboard" 
                              : deviceName;
        yield return new WaitForSeconds(0.5f);
        XRSettings.LoadDeviceByName(device);
        yield return null;
        XRSettings.enabled = true;
    }

    public IEnumerator SwitchTo2DMode() {
        XRSettings.LoadDeviceByName("");
        yield return null;
        XRSettings.enabled = false;
    }
}
