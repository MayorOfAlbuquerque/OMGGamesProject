using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFromCamera : MonoBehaviour {
    Transform cameraTransform;
    Vector3 newRotation;
    Vector3 oldRotation;

    // Use this for initialization
    void Start () {
        cameraTransform = Camera.main.transform;
        newRotation = cameraTransform.eulerAngles;
        oldRotation = newRotation;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);
        transform.rotation = rotation;
    }
}
