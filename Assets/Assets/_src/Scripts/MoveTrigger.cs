using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrigger : MonoBehaviour {
	[SerializeField] GameObject Beacon2;
	
		void OnTriggerEnter(Collider other){
			Debug.Log ("Object is within the collider");
		}

		void OnTriggerStay(Collider other){
			other.gameObject.transform.position = Beacon2.transform.position;
			Debug.Log ("Object is within the trigger");
			
		}

		void OnTriggerExit(Collider other){
			Debug.Log ("Object Exited the trigger");
		}
}
	
