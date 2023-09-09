using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IPetObjectParent
{
    public static PlayerInteraction Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public Counter selectedCounter;
    }

    private InputManager inputManager;
    [SerializeField] private LayerMask counterLayerMask;
    private Counter selectedCounter;
    private Vector3 lastInteractDirection;

    [SerializeField] private Transform playerObjectHoldPosition;
    private Pet petObject;

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

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        inputManager.OnInteractAction += HandleInteractInput;
    }

    void Update()
    {
        HandleInteraction();
    }

#region Counter Handle
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
            if (raycastHit.transform.TryGetComponent(out Counter baseCounter))
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
            selectedCounter.Interact(this);
        }
    }

    private void SetSelectedCounter(Counter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
#endregion
#region Pet Handle

    public Transform GetSurfacePosition()
    {
        return playerObjectHoldPosition;
    }

    public void SetPetObject(Pet _petShopObject)
    {
        this.petObject = _petShopObject;
    }

    public Pet GetPetObject()
    {
        return petObject;
    }

    public void ClearPetObject()
    {
        petObject = null;
    }

    public bool HasPetObject()
    {
        return petObject != null;
    }

#endregion
}
