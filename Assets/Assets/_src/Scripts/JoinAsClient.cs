using UnityEngine;
using UnityEngine.Networking;

public class JoinAsClient : MonoBehaviour
{
    //triggers when another object enters its area.
    private void OnTriggerEnter(Collider other)
    {
        //if object is player then load scene
        if (other.gameObject.tag == "Player")
        {
            if(NetworkManager.singleton.)
            Debug.Log("Joined as client");
            NetworkManager.singleton.StartClient(); //Join as client, only changes to online scene if
        }
    }
}
