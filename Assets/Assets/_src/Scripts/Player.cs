

﻿using System.Collections;
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

    public float speed;
    public Movement movement;

    private void Start()
        {
            mainCamera = Camera.main.gameObject;
            movement = new Movement(speed);
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



	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

        Vector3 newPos;
        
        if (h != 0 || v!= 0)
        {
			Vector3 forward = Camera.main.transform.forward;
			Vector3 right = Camera.main.transform.right;
			newPos = movement.calculate (transform.position,h,v,right,forward);
        }
        else newPos = transform.position;
 
        transform.position = newPos;
    }
		
}