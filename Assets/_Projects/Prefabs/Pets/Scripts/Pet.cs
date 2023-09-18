using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    [SerializeField] private PetObjectSO petObjectSO;
    
    private IPetObjectParent petObjectParent;

    
    public bool isOnCheckInCounter;

    [Header("Happiness Settings")]
    [SerializeField] private PetHappinessBar happinessBar;
    private float decreaseHappinessStartDelay = 6f;
    private float decreaseHappinessRate = 6f;
    private float currentHappiness;
    private float maxHappiness = 3;

    private void Start()
    {
        happinessBar = GetComponentInChildren<PetHappinessBar>();

        currentHappiness = maxHappiness;
    }

    private void Update()
    {
        happinessBar.UpdateHappinessBar(currentHappiness, maxHappiness);
    }

    private void DecreaseHappiness()
    {
        currentHappiness -= 0.5f;

        Debug.Log(gameObject.name + " happiness is " + currentHappiness);

        if (currentHappiness <= 0)
        {
            // TO DO
            // Handle pet's state when happiness reaches 0 or below
            StopDecreaseHappiness();
        }
    }

    public void StartDecreaseHappiness()
    {
        InvokeRepeating("DecreaseHappiness", decreaseHappinessStartDelay, decreaseHappinessRate);
    }

    public void StopDecreaseHappiness()
    {
        CancelInvoke("DecreaseHappiness");
    }

    public PetObjectSO GetPetObjectSO()
    {
        return petObjectSO;
    }

    #region Authority
    public void SetPetObjectParent(IPetObjectParent _petObjectParent)
    {
        if (this.petObjectParent != null)
        {
            this.petObjectParent.ClearPetObject();
        }

        this.petObjectParent = _petObjectParent;

        if (_petObjectParent.HasPetObject())
        {
            Debug.LogError("Counter already has a PetObject!");
        }

        _petObjectParent.SetPetObject(this);

        transform.parent = _petObjectParent.GetSurfacePosition();
        transform.eulerAngles = transform.parent.eulerAngles;
        transform.localPosition = Vector3.zero;
    }

    public IPetObjectParent GetPetObjectParent()
    {
        return petObjectParent;
    }
    #endregion

    #region CheckOuting

    public void CheckoutPet()
    {
        Debug.Log("Pet Checkouted");
        petObjectParent.ClearPetObject();
        Destroy(gameObject);
    }


    #endregion
}
