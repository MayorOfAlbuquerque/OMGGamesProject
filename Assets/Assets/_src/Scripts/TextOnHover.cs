using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TextOnHover : MonoBehaviour, IGvrPointerHoverHandler
{
    [SerializeField] GameObject text;                                                       //This is all going to be shit code
    private int countdown;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //turn off text
        if(text.activeInHierarchy && countdown <= 0)
        {
            text.SetActive(false);
        }
        //turn on/leave text on
        else if(countdown > 0 )
        {
            countdown--;
            if(!text.activeInHierarchy)
            {
                text.SetActive(true);
            }
        }
        text.transform.LookAt(Camera.main.transform);
    }

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hovering to reveal text");

        countdown = 50;
    }

}                                                               // Yep it's a horrible bodge
