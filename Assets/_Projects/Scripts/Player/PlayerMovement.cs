using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputManager inputManager;
    private CharacterController characterController;

    [SerializeField]private float moveSpeed = 5f;
    private float turnSmoothVelocity;
    private float turnSmoothTime = .1f;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector2 moveInput = inputManager.GetMoveInput();
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        float gravity = -9.8f;

        // Calculate rotation angle based on input direction
        if (moveInput != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        // Calculate gravity based on isGrounded condition
        if (!characterController.isGrounded)
        {
            moveDirection.y = gravity;
        } else
        {
            moveDirection.y = -.25f;
        }

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
