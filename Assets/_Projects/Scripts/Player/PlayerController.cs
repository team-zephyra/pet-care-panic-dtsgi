using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    private CharacterController characterController;
    private Animator playerAnimator;

    private Vector2 moveInput;
    private Vector3 moveDirection;

    private float moveSpeed = 5f;
    private float gravity = -9.8f;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;

    private bool isMovePressed;

    private int isWalkingHash;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoveInput();
        HandleGravity();
        HandleAnimation();
    }

    void HandleMoveInput()
    {
        moveInput = inputManager.GetMoveInput();
        moveDirection.x = moveInput.x * moveSpeed;
        moveDirection.z = moveInput.y * moveSpeed;

        isMovePressed = moveInput.x != 0 || moveInput.y != 0;

        // Calculate rotation angle based on input direction
        if (moveInput != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleAnimation()
    {
        // Get parameters from Animator Controller
        bool isWalking = playerAnimator.GetBool(isWalkingHash);

        if (isMovePressed && !isWalking)
        {
            playerAnimator.SetBool(isWalkingHash, true);    
        } else if (!isMovePressed && isWalking)
        {
            playerAnimator.SetBool(isWalkingHash, false);
        }
    }

    void HandleGravity()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y = gravity;
        } else
        {
            moveDirection.y = -.25f;
        }
    }
}
