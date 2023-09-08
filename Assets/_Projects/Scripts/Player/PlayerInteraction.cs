using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IPetShopObjectParent
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
    private PetShopObject petShopObject;

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

    public Transform GetPetShopObjectFollowTransform()
    {
        return playerObjectHoldPosition;
    }

    public void SetPetShopObject(PetShopObject petShopObject)
    {
        this.petShopObject = petShopObject;
    }

    public PetShopObject GetPetShopObject()
    {
        return petShopObject;
    }

    public void ClearPetShopObject()
    {
        petShopObject = null;
    }

    public bool HasPetShopObject()
    {
        return petShopObject != null;
    }
}
