using UnityEngine;
using UnityEngine.Networking;

public class JoinAsServer : MonoBehaviour
{
    //triggers when another object enters its area.
    private void OnTriggerEnter(Collider other)
    {
        //if object is player then load scene
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Joined as dedicated server");
            NetworkManager.singleton.StartServer(); //Starts as server, NetworkManger loads online scene
        }
    }
}
