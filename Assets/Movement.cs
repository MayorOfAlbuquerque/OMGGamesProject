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

        Vector3 targetDirection = new Vector3(h, transform.position.y, v);

        //Vector3 targetDirection = transform.position;

        targetDirection = Camera.main.transform.TransformDirection(targetDirection);
        targetDirection.x += (h* 0.1f);
        targetDirection.z += (v* 0.1f);

        

        //targetDirection += Camera.main.transform.TransformDirection(h * 0.1f, 0f, v * 0.1f);

        transform.position = targetDirection;
    }
}
