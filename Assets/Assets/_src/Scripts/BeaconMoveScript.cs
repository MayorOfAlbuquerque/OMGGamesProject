using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BeaconMoveScript : MonoBehaviour {
	AndroidJavaClass jc;
	string javaMessage;
	[SerializeField]
	public GameObject Beacon1;
	[SerializeField]
	public GameObject Beacon2;
	private string lastReceived = "1";
	[SerializeField]
	public GameObject Beacon3;

	// Use this for initialization
	void Start () {

		// Acces the android java receiver we made
		jc = new AndroidJavaClass("com.example.omg.myapplication.UnityReceiver");
		Debug.LogError (jc);
		// We call our java class function to create our MyReceiver java object
		jc.CallStatic("createInstance");
		
	}
	// Update is called once per frame
	void Update () {
		javaMessage = jc.GetStatic<string> ("intentMessage");
		switch (javaMessage) {
			case "1":
				if (lastReceived != "1") {
				this.gameObject.GetComponent<CharacterController> ().enabled = false;
					this.gameObject.transform.position = Beacon1.transform.position;
				this.gameObject.GetComponent<CharacterController> ().enabled = true;
					lastReceived = "1";
				}
				break;
			case "2":
				if (lastReceived != "2") {
				this.gameObject.GetComponent<CharacterController> ().enabled = false;
					this.gameObject.transform.position = Beacon2.transform.position;
				this.gameObject.GetComponent<CharacterController> ().enabled = true;
					lastReceived = "2";
				}
				break;
			case "3":
				if (lastReceived != "3") {
				this.gameObject.GetComponent<CharacterController> ().enabled = false;
					this.gameObject.transform.position = Beacon3.transform.position;
				this.gameObject.GetComponent<CharacterController> ().enabled = true;
					lastReceived = "3";
				}
				break;
			default:
				//Stay where you are
				break;
		}
	}
}
