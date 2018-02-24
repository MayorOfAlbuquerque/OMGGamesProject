using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconMoveScript : MonoBehaviour {
	AndroidJavaClass jc;
	string javaMessage;
	[SerializeField]
	public GameObject Beacon1;
	[SerializeField]
	public GameObject Beacon2;
	private string lastReceived = "1";
	public GameObject cube;


	// Use this for initialization
	void Start () {
		cube = GameObject.FindWithTag("Test");

		// Acces the android java receiver we made
		jc = new AndroidJavaClass("com.example.omg.myapplication.UnityReceiver");
		Debug.LogError (jc);
		// We call our java class function to create our MyReceiver java object
		jc.CallStatic("createInstance");
		
	}
	// Update is called once per frame
	void Update () {
		javaMessage = jc.GetStatic<string> ("intentMessage");
		if ((javaMessage == "1") && (lastReceived != "1")) {
			cube.SetActive (true);
			this.gameObject.GetComponent<CharacterController> ().enabled = false;
			this.gameObject.transform.position = Beacon1.transform.position;
			this.gameObject.GetComponent<CharacterController> ().enabled = true;
			lastReceived = "1";
		} else if ((javaMessage == "2") && (lastReceived!="2")) {
			cube.SetActive (false);
			this.gameObject.GetComponent<CharacterController> ().enabled = false;
			this.gameObject.transform.position = Beacon2.transform.position;
			this.gameObject.GetComponent<CharacterController> ().enabled = true;
			lastReceived = "2";
		}

		
	}
}
