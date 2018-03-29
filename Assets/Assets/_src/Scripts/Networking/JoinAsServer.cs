
using UnityEngine;
using UnityEngine.Networking;

public class JoinAsServer : MonoBehaviour
{
    //triggers when another object enters its area.
    private void OnTriggerEnter(Collider other)
    {
        if(NetworkManager.singleton == null) {
            return;
        }
        Debug.Log("join as server trigger enter");
        //if object is player then load scene
        if (other.gameObject.tag == "Player")
        {
			bool started;
			started = NetworkManager.singleton.StartServer(); //Starts as server, NetworkManger loads online scene
			if (started) {
				Debug.Log ("Successfully started the Server");
			} 
			else
			{
				Debug.Log ("Failed to Start Server");
			}
        }
    }
}
