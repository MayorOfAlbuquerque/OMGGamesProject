
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum Weapon{
    Spanner, Candlestick, NONE
}


public class Player : NetworkBehaviour {

    private Weapon weapon;
    private GameObject weaponModel;
    [SerializeField] int health;

    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool> { }

    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleRemote;
    [SerializeField] ToggleEvent onToggleLocal;
    GameObject mainCamera;
    public float speed;

    private void Start()
        {
            mainCamera = Camera.main.gameObject;
            EnablePlayer();
            this.weapon = Weapon.NONE;
            HideModelIfLocal();
        }
    private void HideModelIfLocal()
    {
        if(!isLocalPlayer) 
        {
            return;
        }
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach(var meshRenderer in renderers) 
        {
            if (!meshRenderer.gameObject.name.Equals("GvrReticlePointer"))
            {
                meshRenderer.receiveShadows = false;
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
        }
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


    public void OpenDoor(GameObject thisDoor)
    {
        CmdServerOpenDoor(thisDoor);
    }

    /* This is to open doors: */
    [Command]
    public void CmdServerOpenDoor(GameObject thisDoor)
    {
        RpcClientOpenDoor(thisDoor);

    }

    [ClientRpc]
    public void RpcClientOpenDoor(GameObject thisDoor)
    {
        thisDoor.gameObject.GetComponent<AnimationTrigger>().PlayAnimation();
    }

}