using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BeaconMoveScript : MonoBehaviour
{
    AndroidJavaClass jc;
    string javaMessage;
	private GameObject Beacon1;
    private GameObject Beacon2;
    private GameObject Beacon3;
	private GameObject Beacon4;
	private GameObject Beacon5;
	private string lastReceived = "1";

    private Dictionary<string, GameObject> beaconAndIds
        = new Dictionary<string, GameObject>();

	private Dictionary<string, GameObject> carriageBeaconId
	= new Dictionary<string, GameObject> ();

	private GameObject lounge;
	private GameObject bed1;
	private GameObject bed2;
	private GameObject kitchen;
	private GameObject dining;

    public bool shouldSearch = true;

    // Use this for initialization
    void Start()
    {
		// Get the carriages
		Beacon1 = GameObject.Find("Beacon_Kitchen");
		Beacon2 = GameObject.Find ("Beacon_Bar");
		Beacon3 = GameObject.Find ("Beacon_Bedrooms_1");
		Beacon4 = GameObject.Find ("Beacon_Library");
		Beacon5 = GameObject.Find ("Beacon_Bedrooms_2");

		lounge  = GameObject.FindGameObjectWithTag ("lounge");
		kitchen = GameObject.FindGameObjectWithTag ("kitchen");
		bed1    = GameObject.FindGameObjectWithTag ("bed1");
		bed2    = GameObject.FindGameObjectWithTag ("bed2");
		dining  = GameObject.FindGameObjectWithTag ("dining");

		carriageBeaconId ["1"] = kitchen;
		carriageBeaconId ["2"] = dining;
		carriageBeaconId ["3"] = bed1;
		carriageBeaconId ["4"] = lounge;
		carriageBeaconId ["5"] = bed2;

        #if UNITY_ANDROID
        // Acces the android java receiver we made
        jc = new AndroidJavaClass("com.example.omg.myapplication.UnityReceiver");
        Debug.LogError(jc);
        // We call our java class function to create our MyReceiver java object
        jc.CallStatic("createInstance");
        #endif
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
    /* B1 = Kitchen 
	   B2 = Dining
	   B3 = Bed1
	   B4 = Library/lounge
	   B5 = Bed2
	*/
    void Update()
    {

        if (shouldSearch)
        {


#if UNITY_ANDROID
            javaMessage = jc.GetStatic<string>("intentMessage");
            if (lastReceived == javaMessage)
            {
                return;
            }

			GameObject beacon;
			if(beaconAndIds.ContainsKey(javaMessage)){
				beacon = beaconAndIds[javaMessage];
			}
			else{
				beacon = null;
			}

			GameObject carriage; 
			if(carriageBeaconId.ContainsKey(javaMessage)){
				carriage = carriageBeaconId[javaMessage];
			}
			else{
				carriage = null;
			}

            if (beacon == null || carriage == null)
            {
                return;
            }

            carriage.SetActive(true);
            this.gameObject.GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.position = beacon.transform.position;
            this.gameObject.GetComponent<CharacterController>().enabled = true;

            foreach (var entry in carriageBeaconId)
            {
                if (entry.Key != javaMessage)
                {
                    entry.Value.SetActive(false);
                }
            }
            lastReceived = javaMessage;
#endif
        }
    }


    public void EnableSearch()
    {
        shouldSearch = true;
    }

    public void StopSearch()
    {
        shouldSearch = false;
    }
}
