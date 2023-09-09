using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPetObjectParent
{
    public Transform GetSurfacePosition();

    public void SetPetObject(Pet petShopObject);

    public Pet GetPetObject();

    public void ClearPetObject();

    public bool HasPetObject();
}
