using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public float speed;
	public Movement movement;

	void Start () {
		movement = new Movement (speed);
	}

	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

        Vector3 newPos;
        
        if (h != 0 || v!= 0)
        {
			Vector3 right = Camera.main.transform.right;
			Debug.Log ("Right: ");
			Debug.Log (right);
			Vector3 forward = Camera.main.transform.forward;
			Debug.Log ("Forward: ");
			Debug.Log (forward);
			newPos = movement.calculate (transform.position,h,v,right,forward);
        }
        else newPos = transform.position;
 
        transform.position = newPos;
    }
		
}
