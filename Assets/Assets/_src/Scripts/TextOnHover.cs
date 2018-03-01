using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextOnHover : MonoBehaviour, IGvrPointerHoverHandler
{
    [SerializeField] GameObject text;                                                       //This is all going to be shit code
    [SerializeField] int timerValue;
    private int countdown;

    // Use this for initialization
    void Start()
    {
        this.text = transform.GetChild(0).gameObject;
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
        //Makes text face wrong way so text must be contained within seperate unity object where it is reversed 180
        text.transform.LookAt(Camera.main.transform);
    }

    //while looking at object reset timer value to max
    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hovering to reveal text");

        countdown = timerValue;
    }

    public void ChangeText(string newText)
    {
        Debug.Log(newText);
        text.SetActive(true);
        TextMesh txt = text.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        text.SetActive(false);
        txt.text = newText;
    }

}                                                               // Yep it's a horrible bodge
