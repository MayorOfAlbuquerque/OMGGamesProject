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
        newRotation = cameraTransform.localEulerAngles;
        oldRotation = newRotation;
    }
	
	// Update is called once per frame
	void Update () {

        newRotation = cameraTransform.localEulerAngles;
        if (newRotation != oldRotation)
        {
            transform.Rotate(newRotation.x, newRotation.y, newRotation.z, Space.World);
            oldRotation = newRotation;
        }
    }
}
