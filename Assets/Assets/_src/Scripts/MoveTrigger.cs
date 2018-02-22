using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrigger : MonoBehaviour {
	[SerializeField] GameObject Beacon2;
	
		void OnTriggerEnter(Collider other){
			Vector3 moveVector = Beacon2.transform.position - other.gameObject.transform.position;
			other.gameObject.GetComponent<CharacterController> ().Move (moveVector);
			Debug.Log ("Object is within the collider");
		}

		void OnTriggerStay(Collider other){
//			other.gameObject.transform.position = Beacon2.transform.position;
			Debug.Log ("Object is within the trigger");
		}

		void OnTriggerExit(Collider other){
			Debug.Log ("Object Exited the trigger");
		}
}
	
