using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconMoveScript : MonoBehaviour {
	AndroidJavaClass jc;
	int javaMessage;
	[SerializeField]
	public GameObject Beacon1;
	[SerializeField]
	public GameObject Beacon2;
	private int lastReceived = 1;
	private Vector3 B1;
	private Vector3 B2;
	public GameObject cube;
	[SerializeField]
	public GameObject player;


	// Use this for initialization
	void Start () {
		cube = GameObject.FindGameObjectWithTag ("Test");
		// Acces the android java receiver we made
		jc = new AndroidJavaClass("com.example.omg.myapplication.UnityReceiver");
		//Debug.LogError (jc);
		// We call our java class function to create our MyReceiver java object
		jc.CallStatic("createInstance");
		B1 = Beacon1.transform.position;
		B2 = Beacon1.transform.position;
		
	}
	// Update is called once per frame
	void Update () {
		int.TryParse(jc.GetStatic<string> ("intentMessage"), out javaMessage);
		if ((javaMessage == 1) && (lastReceived != 1)) {
			cube.SetActive (true);
			gameObject.transform.position = new Vector3 (0, 0, 0);
			lastReceived = 1;
		} else if ((javaMessage == 2) && (lastReceived!=2)) {
			cube.SetActive (false);
			gameObject.transform.position = new Vector3 (100, 100, 100);
			lastReceived = 2;
		}

		
	}
}
