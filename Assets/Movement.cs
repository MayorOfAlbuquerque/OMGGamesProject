using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

       Vector3 targetDirection = new Vector3(h, 0f, v); // Initialised the inital targetDirection 

        //Vector3 targetDirection = transform.position; // Causes jittering in the motion

        targetDirection = Camera.main.transform.TransformDirection(targetDirection);  // Transforming the direction to the Camera's
        targetDirection.x += (h* 0.1f); // Moving it horizontally 
        targetDirection.z += (v* 0.1f); // Moving it vertically 
        targetDirection.y = transform.position.y; // Keeping y constant (without this, the object seems to fall constantly?)

        transform.position = targetDirection;
    }
}
