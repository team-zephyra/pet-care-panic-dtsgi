using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetShopObject : MonoBehaviour
{
    [SerializeField] private PetShopObjectSO petShopObjectSO;

    private IPetShopObjectParent petShopObjectParent;

    public PetShopObjectSO GetPetShopObjectSO()
    {
        return petShopObjectSO;
    }

    public void SetPetShopObjectParent(IPetShopObjectParent petShopObjectParent)
    {
        if (this.petShopObjectParent != null)
        {
            this.petShopObjectParent.ClearPetShopObject();
        }

        this.petShopObjectParent = petShopObjectParent;

        if (petShopObjectParent.HasPetShopObject())
        {
            Debug.LogError("Counter already has a PetShopObject!");
        }

        petShopObjectParent.SetPetShopObject(this);

        transform.parent = petShopObjectParent.GetPetShopObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IPetShopObjectParent GetPetShopObjectParent()
    {
        return petShopObjectParent;
    }

    public void DestroySelf()
    {
        
    }
}
