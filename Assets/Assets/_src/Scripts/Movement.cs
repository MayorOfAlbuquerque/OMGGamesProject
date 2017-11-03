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

        h = -1 * h;

        Vector3 newPos;
        
        if (h != 0 || v!= 0)
        {
            newPos = transform.position + (Camera.main.transform.right * h * 0.1f);
            newPos = newPos + Camera.main.transform.forward * v * 0.1f;
            newPos.y = 1f; // This is to avoid flying
        }
       // else if (v != 0)
        //{
          
        //}
        else newPos = transform.position;
 
        transform.position = newPos;
   
    }
}
