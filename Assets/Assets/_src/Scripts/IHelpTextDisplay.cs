using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IHelpTextDisplay : IEventSystemHandler
 {
    void Show(string text);
    void Hide();
}