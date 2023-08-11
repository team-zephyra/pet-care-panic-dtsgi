using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    private InputManager inputManager;
    private CharacterController characterController;
    private Animator playerAnimator;
    [SerializeField] private LayerMask counterLayerMask;

    private Vector3 lastInteractDirection;

    private ClearCounter selectedCounter;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private bool isMovePressed;

    private int isWalkingHash;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is already an instance of PlayerController.");
        }
        else
        {
            Instance = this;
        }

        if (inputManager == null)
        {
            inputManager = FindObjectOfType<InputManager>();
        }

        if (inputManager == null)
        {
            Debug.Log("PlayerController script requires InputManager in order to work");
        }
       
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        isWalkingHash = Animator.StringToHash("isWalking");

        inputManager.OnInteractAction += HandleInteractInput;
    }

    private void HandleInteractInput(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    void Update()
    {
        HandleMoveInput();
        HandleInteraction();
        HandleAnimation();
    }

    void HandleMoveInput()
    {
        Vector2 moveInput = inputManager.GetMoveInput();
        Vector3 moveDirection;

        isMovePressed = moveInput.x != 0 || moveInput.y != 0;

        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        float playerRadius = .4f;
        float playerHeight = 1.5f;
        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            // Cannot move towards moveDirection, therefore:

            // Attempt to move only on moveDirection.x
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                // Only move on the X
                moveDirection = moveDirectionX;
            }
            else
            {
                // Cannot only move on the X, therefore:

                // Attempt to move only on moveDirection.z
                Vector3 moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                {
                    // Only move on the Z
                    moveDirection = moveDirectionZ;
                }
                else
                {
                    // Cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }

    void HandleInteraction()
    {
        Vector2 moveInput = inputManager.GetMoveInput(); ;
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        RaycastHit raycastHit;

        float maxDistance = 2f;

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        if (Physics.Raycast(transform.position, lastInteractDirection, out raycastHit, maxDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            } else
            {
                SetSelectedCounter(null);
            }
        } else
        {
            SetSelectedCounter(null);
        }
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

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
