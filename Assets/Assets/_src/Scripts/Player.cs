
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
//using DoorController;

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

    public void ChangeHealth(int change) //---------------------------------------TODO
    {
        this.health = health + change;
        if(this.health <= 0)
        {
            //remove player model so player is invisible
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            //kill player
            //Make Ghost
            //set killable script to false
        }
    }

    [Command]
    public void CmdAttackPlayer(GameObject otherPlayer)
    {
        RpcRemovePlayerHealth(otherPlayer);
    }

    [ClientRpc]
    private void RpcRemovePlayerHealth(GameObject otherPlayer)
    {
        Player playerScript = otherPlayer.GetComponent<Player>();
        playerScript.ChangeHealth(-100);
    }



    /*
	 * Wrapper function for the open door command
	*/
    public void PlayerOpenDoor(GameObject door)
	{
		CmdOpenDoor (door);
	}

	/*
	 * Wrapper function for the close door command
	*/
	public void PlayerCloseDoor(GameObject door)
	{
		CmdCloseDoor (door);
	}

	/*
	 * Forces the server to open the door, this calls the RPC so animation is synced for all clients
	*/
	[Command]
	public void CmdOpenDoor(GameObject door){
		door.GetComponent<DoorController>().RpcOpenDoor (); //Synchronise this change for all other clients
	}
		

	/*
	 * Forces the server to close the door, this calls the RPC so animation is synced for all clients
	*/
	[Command]
	public void CmdCloseDoor(GameObject door){
		door.GetComponent<DoorController>().RpcCloseDoor ();
	}

    public Weapon GetPlayerCurrentWeapon()
    {
        return weapon;
    }

    [Command]
    public void CmdChangeSpawnWeapon(GameObject spawner, Weapon weapon)
    {
        spawner.GetComponent<PickupController>().RpcSetSpawnWeaponModel(weapon);
    }

    [Command]
    public void CmdSetPlayerWeapon(Weapon weapon)
    {
        RpcNetworkWeaponAppearence(weapon, this.gameObject);
    }

    [ClientRpc]
    public void RpcNetworkWeaponAppearence(Weapon weapon, GameObject player)
    {
        this.weapon = weapon;

        if (weapon != Weapon.NONE)
        {
            Destroy(weaponModel);
        }
        weaponModel = Instantiate(Resources.Load(weapon.ToString(), typeof(GameObject))) as GameObject;
        weaponModel.transform.SetParent(player.transform.GetChild(0).gameObject.transform.GetChild(0).transform, false);
    }
}