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

        newRotation = cameraTransform.eulerAngles;
        if (newRotation != oldRotation)
        {
            float diffX = Mathf.DeltaAngle(oldRotation.x, newRotation.x);
            float diffY = Mathf.DeltaAngle(oldRotation.y, newRotation.y);
            float diffZ = 0.0f; //Mathf.DeltaAngle(oldRotation.z, newRotation.z);
            transform.Rotate(diffX, diffY, diffZ, Space.World);
            oldRotation = newRotation;
        }
    }
}
