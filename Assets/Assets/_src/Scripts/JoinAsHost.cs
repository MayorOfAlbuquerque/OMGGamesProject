using UnityEngine;
using UnityEngine.Networking;

public class JoinAsHost : MonoBehaviour
{

    //triggers when another object enters its area.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Host();
        }
    }

    public void Host() {
         Debug.Log("Joined as dedicated server");
         NetworkManager.singleton.StartHost(); //Starts as host, NetworkManger loads online scene
    }
}
