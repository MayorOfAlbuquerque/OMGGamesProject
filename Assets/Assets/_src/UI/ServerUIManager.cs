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

    public void CameraFollowRandomPlayer() {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        if(playerObjects.Length < 1) {
            return;
        }

        int randomIndex = new System.Random().Next(0, playerObjects.Length);
        spectatorCameraController?.SetCameraMode(CameraMovementMode.Follow);
        Debug.Log("Following Player: " + playerObjects[randomIndex].name);
        spectatorCameraController?.SetObjectToFollow(playerObjects[randomIndex]);
    }
}
