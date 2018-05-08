using UnityEngine;
using System.Collections;

/// <summary>
/// Event handlers. Each method maps to a button on the bluetooth controller
/// </summary>
public interface IControllerInteractable {
    /// <summary>
    /// Standard main button click.
    /// </summary>
    void OnClick();
    void OnKeyDown(KeyCode code);
    void OnKeyUp(KeyCode code);
    void OnHoverEnter();
    void OnHoverExit();
}