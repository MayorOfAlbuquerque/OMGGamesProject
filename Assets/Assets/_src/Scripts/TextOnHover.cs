using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextOnHover : MonoBehaviour, IGvrPointerHoverHandler
{
    [SerializeField] GameObject textHodler;                                                       //This is all going to be shit code
    [SerializeField] int timerValue;
    private int countdown;

    // Use this for initialization
    void Start()
    {
        this.textHodler = transform.GetChild(0).gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        if(textHodler == null || Camera.main == null) {
            return;
        }
        //turn off text
        if(textHodler.activeInHierarchy && countdown <= 0)
        {
            textHodler.SetActive(false);
        }
        //turn on/leave text on
        else if(countdown > 0 )
        {
            countdown--;
            if(!textHodler.activeInHierarchy)
            {
                textHodler.SetActive(true);
            }
        }
        //Makes text face wrong way so text must be contained within seperate unity object where it is reversed 180
        textHodler.transform.LookAt(Camera.main.transform);
    }

    //while looking at object reset timer value to max
    public void OnGvrPointerHover(PointerEventData eventData)
    {
        Debug.Log("Hovering to reveal text");

        countdown = timerValue;
    }

    public void ChangeText(string newText)
    {
        if(textHodler == null)
        {
            this.textHodler = transform.GetChild(0).gameObject;
        }
        Debug.Log(newText);
        textHodler.SetActive(true);
        Text txt = textHodler.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>();
        txt.text = newText;
        textHodler.SetActive(false);
    }
}                                                               // Yep it's a horrible bodge
