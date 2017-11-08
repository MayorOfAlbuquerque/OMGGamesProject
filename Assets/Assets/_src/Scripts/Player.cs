using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public float Speed;
	public Movement Movement;

	void Start () {
		Movement = new Movement (Speed);
	}

	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

        Vector3 newPos;
        
        if (h != 0 || v!= 0)
        {
			Vector3 right = Camera.main.transform.right;
			Vector3 forward = Camera.main.transform.forward;
			newPos = Movement.Calculate (transform.position,h,v,right,forward);
        }
        else newPos = transform.position;
 
        transform.position = newPos;
    }
		
}
