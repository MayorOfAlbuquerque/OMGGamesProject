using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MoveTrigger : MonoBehaviour {
	[SerializeField] GameObject Beacon2;
	
		void OnTriggerEnter(Collider other){
		//We know that other is the character controller.
		Debug.LogError(other);
		other.GetComponent<CharacterController> ().enabled = false;
		other.transform.position = Beacon2.transform.position;
		other.GetComponent<CharacterController> ().enabled = true;
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
	
