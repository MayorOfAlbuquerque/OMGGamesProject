using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDriver : MonoBehaviour {
    public Rigidbody trainEngine;
    public float speed;
	// Use this for initialization
	void Start () {
        if (speed < 0.1f) {
            speed = 1.0f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (trainEngine != null)
        {
            trainEngine.velocity = Vector3.forward  * -speed;
        }
	}
}
