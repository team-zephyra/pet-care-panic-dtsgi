using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.GameplayActions gameplayActions;

    private Vector2 moveInput;
    
    void Awake()
    {
        playerInput = new PlayerInput();
        gameplayActions = playerInput.Gameplay;

        gameplayActions.Move.started += GetMoveInput;
        gameplayActions.Move.performed += GetMoveInput;
        gameplayActions.Move.canceled += GetMoveInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
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
