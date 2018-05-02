using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class VRModeManager : MonoBehaviour {

    public string deviceName;
	// Use this for initialization
	void Start () {
        StartCoroutine(SwitchToVRMode(deviceName));
	}

    public static IEnumerator SwitchToVRMode(string deviceName) {
        if(XRSettings.enabled 
           && XRSettings.loadedDeviceName.Equals(deviceName)) {
            yield break;
        }
        string device = string.IsNullOrEmpty(deviceName) 
                              ? "cardboard" 
                              : deviceName;
        yield return new WaitForSeconds(0.5f);
        XRSettings.LoadDeviceByName(device);
        yield return null;
        XRSettings.enabled = true;
    }

    public static IEnumerator SwitchTo2DMode() {
        XRSettings.LoadDeviceByName("");
        yield return null;
        XRSettings.enabled = false;
    }
}
