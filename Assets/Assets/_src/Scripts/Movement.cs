using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement{
	public float speed;

	public Movement(float spd){
		speed = spd;
	}

	public Vector3 Calculate(Vector3 position, float h, float v, Vector3 right, Vector3 forward){
		Vector3 newPos = position + (right * h * speed);
		newPos = newPos + (forward * v * speed);
		newPos.y = 1f;
		return newPos;
	}
}
