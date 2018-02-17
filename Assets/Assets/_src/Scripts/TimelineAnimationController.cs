using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayableDirector))]
public class TimelineAnimationController : InteractableObjectController, IDoor
{
    PlayableDirector pd;

    public bool IsOpen
    {
        get
        {
            return false;
        }
    }

    void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    public void Close()
    {
    }

    public override void OnClick()
    {
        pd.Play();
        RpcPlayAnimationOnNetwork();
    }

    [ClientRpc]
    private void RpcPlayAnimationOnNetwork()
    {
        pd.Play();
    }


    public override void OnKeyDown(KeyCode code)
    {

    }

    public override void OnKeyUp(KeyCode code)
    {
        throw new System.NotImplementedException();
    }

    public void Open()
    {
        pd.Play();
    }

    public void Toggle()
    {
        pd.Play();
    }

}
