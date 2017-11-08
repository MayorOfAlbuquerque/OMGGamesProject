using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;


public class Player : NetworkBehaviour {

    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool> { }

        [SerializeField] ToggleEvent onToggleShared;
        [SerializeField] ToggleEvent onToggleRemote;
        [SerializeField] ToggleEvent onToggleLocal;
        GameObject mainCamera;


        private void Start()
        {
            mainCamera = Camera.main.gameObject;
            EnablePlayer();

        }

        private void EnablePlayer()
        {
            onToggleShared.Invoke(true);
            if (isLocalPlayer)
            {
                mainCamera.SetActive(false);
                onToggleLocal.Invoke(true);
            }
            else
            {
                onToggleRemote.Invoke(true);
            }
        }

        private void DisablePlayer()
        {
            onToggleShared.Invoke(false);
            if (isLocalPlayer)
            {
                mainCamera.SetActive(true);
                onToggleLocal.Invoke(false);
            }
            else
            {
                onToggleRemote.Invoke(false);
            }
        }

    }
