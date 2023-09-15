using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [SerializeField] private PetObjectSO petObjectSO;
    private IPetObjectParent petObjectParent;

    public float happiness = 3;
    public bool isOnCheckInCounter;

    

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
