using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpTextCanvasDisplay : MonoBehaviour, IHelpTextDisplay
{
    public Text TextComponent;

    public void Show(string text) {
        if(TextComponent == null) {
            return;
        }
        TextComponent.text = text;
        TextComponent.enabled = true;
    }
    public void Hide(){
        TextComponent.enabled = false;
    }
}
