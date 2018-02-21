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
		int.TryParse(jc.GetStatic<string> ("intentMessage"), out javaMessage);
		if ((javaMessage == 1) && (lastReceived != 1)) {
			this.gameObject.transform.position = Beacon1.transform.position;
			lastReceived = 1;
		} else if ((javaMessage == 2) && (lastReceived!=2)) {
			this.gameObject.transform.position = Beacon2.transform.position;
			lastReceived = 2;
		}

		
	}
}
