using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class stores relevant information about a page of UI
/// </summary>
public class UIPage : MonoBehaviour
{
    [Tooltip("The default UI to have selected when opening this page")]
    public GameObject defaultSelected;

    /// <summary>
    /// Description:
    /// Sets the currently selected UI to the one defaulted by this UIPage
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    public void SetSelectedUIToDefault()
    {
        if (UIMainMenu.instance != null && defaultSelected != null)
        {
            UIMainMenu.instance.eventSystem.SetSelectedGameObject(defaultSelected);
        }
        else if (UIMainMenu.instance != null && defaultSelected != null)
        {
            UIMainMenu.instance.eventSystem.SetSelectedGameObject(null);
        }
    }
}
