using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInCounter : Counter
{
    [SerializeField] private PetShopObjectSO petShopObjectSO;

    public override void Interact(PlayerInteraction player)
    {
        if (!HasPetShopObject() && !player.HasPetShopObject())
        {
            // Counter does not have a PetShopObject and Player is not carrying anything
            // Can spawn PetShopObject
            Transform petShopObjectTransform = Instantiate(petShopObjectSO.prefab, GetPetShopObjectFollowTransform());
            petShopObjectTransform.GetComponent<PetShopObject>().SetPetShopObjectParent(this);
        }
        else if (HasPetShopObject() && !player.HasPetShopObject())
        {
            // Counter have a PetShopObject and Player is not carrying anything
            // Give PetShopObject to Player
            GetPetShopObject().SetPetShopObjectParent(player);
        }
        else if (!HasPetShopObject() && player.HasPetShopObject())
        {
            // Counter does not have a PetShopObject and Player is carrying something
            // Can put "something" to Counter
            player.GetPetShopObject().SetPetShopObjectParent(this);
        }
    }
}
