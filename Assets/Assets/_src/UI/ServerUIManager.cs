using UnityEngine;
using System.Collections;


public class ServerUIManager : MonoBehaviour
{
    public SpectatorCameraController spectatorCameraController;

    public void CameraFollowPlayer(int playerIndex) {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        if(playerObjects.Length < playerIndex) {
            return;
        }
        spectatorCameraController?.SetCameraMode(CameraMovementMode.Follow);
        Debug.Log("Following Player: " + playerObjects[playerIndex].name);
        spectatorCameraController?.SetObjectToFollow(playerObjects[playerIndex]);
    }
}
