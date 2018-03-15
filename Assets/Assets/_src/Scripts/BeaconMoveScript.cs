using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BeaconMoveScript : MonoBehaviour
{
    AndroidJavaClass jc;
    string javaMessage;
    [SerializeField]
    public GameObject Beacon1;
    [SerializeField]
    public GameObject Beacon2;
    private string lastReceived = "1";
    [SerializeField]
    public GameObject Beacon3;
	[SerializeField]
	public GameObject Beacon4;
	[SerializeField]
	public GameObject Beacon5;

    private Dictionary<string, GameObject> beaconAndIds
        = new Dictionary<string, GameObject>();

    // Use this for initialization
    void Start()
    {

        // Acces the android java receiver we made
        jc = new AndroidJavaClass("com.example.omg.myapplication.UnityReceiver");
        Debug.LogError(jc);
        // We call our java class function to create our MyReceiver java object
        jc.CallStatic("createInstance");

        if(Beacon1 != null)
            beaconAndIds.Add("1", Beacon1);
        if (Beacon2 != null)
            beaconAndIds.Add("2", Beacon2);
        if (Beacon3 != null)
            beaconAndIds.Add("3", Beacon3);
        if (Beacon4 != null)
            beaconAndIds.Add("4", Beacon4);
        if (Beacon5 != null)
            beaconAndIds.Add("5", Beacon5);

    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
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
			case "4":
				if (lastReceived != "4") {
					this.gameObject.GetComponent<CharacterController> ().enabled = false;
					this.gameObject.transform.position = Beacon4.transform.position;
					this.gameObject.GetComponent<CharacterController> ().enabled = true;
					lastReceived = "4";
				}
				break;
			case "5":
				if (lastReceived != "5") {
					this.gameObject.GetComponent<CharacterController> ().enabled = false;
					this.gameObject.transform.position = Beacon5.transform.position;
					this.gameObject.GetComponent<CharacterController> ().enabled = true;
					lastReceived = "5";
				}
				break;
			default:
				//Stay where you are
				break;
		}
#endif
	}
}
