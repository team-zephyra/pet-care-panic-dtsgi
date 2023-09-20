using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event EventHandler OnInteractAction;

    private PlayerInput playerInput;
    private PlayerInput.GameplayActions gameplayActions;
    
    void Awake()
    {
        playerInput = new PlayerInput();
        gameplayActions = playerInput.Gameplay;

        gameplayActions.Interact.performed += OnInteractInputPerformed;
    }

    private void OnInteractInputPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMoveInput()
    {
        Vector2 moveInputVector = gameplayActions.Move.ReadValue<Vector2>();

        return moveInputVector;
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }
}
