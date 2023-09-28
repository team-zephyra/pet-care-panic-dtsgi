using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTutorial : MonoBehaviour
{

    public Button confirmButton;
    private InputManager inputManager;
    private PlayerInput playerInput;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        playerInput = inputManager.GetPlayerInput();
    }

    private void Update()
    {
        if (playerInput.Gameplay.Confirm.triggered)
        {
            if (confirmButton != null)
            {
                confirmButton.onClick.Invoke();
            }
            else
            {
                Debug.LogWarning("ConfirmButton has not been assigned.");
            }
        }
    }

}
