using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    private InputManager inputManager;
    [SerializeField] private LayerMask counterLayerMask;
    private ClearCounter selectedCounter;
    private Vector3 lastInteractDirection;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is already an instance of PlayerInteraction.");
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        inputManager.OnInteractAction += HandleInteractInput;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInteraction();
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
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleInteractInput(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
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
