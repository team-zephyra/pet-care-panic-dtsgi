using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPetShopObjectParent
{
    public Transform GetPetShopObjectFollowTransform();

    public void SetPetShopObject(PetShopObject petShopObject);

    public PetShopObject GetPetShopObject();

    public void ClearPetShopObject();

    public bool HasPetShopObject();
}
