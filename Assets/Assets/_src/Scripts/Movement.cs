using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement {
	private float speed;

	public Movement(float spd){
		speed = spd;
	}

	public Vector3 calculate(Vector3 position, float h, float v, Vector3 right, Vector3 forward){
		Vector3 newPos = position + (right * h * speed * Time.deltaTime);
		newPos = newPos + (forward * v * speed * Time.deltaTime);
		newPos.y = 1f;
		return newPos;
	}
}
