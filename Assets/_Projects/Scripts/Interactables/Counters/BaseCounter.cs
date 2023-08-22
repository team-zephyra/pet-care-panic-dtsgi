using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IPetShopObjectParent
{
    [SerializeField] private Transform counterSurfacePosition;
    private PetShopObject petShopObject;

    public virtual void Interact(PlayerInteraction playerInteraction)
    {
        // Child classes should override this method to implement their own logic
    }

    public Transform GetPetShopObjectFollowTransform()
    {
        return counterSurfacePosition;
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
