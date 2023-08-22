using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private PetShopObjectSO petShopObjectSO;

    public override void Interact(PlayerInteraction player)
    {
        if (!HasPetShopObject())
        {
            // There is no PetShopObject
            if (player.HasPetShopObject())
            {
                // Player is carrying something
                player.GetPetShopObject().SetPetShopObjectParent(this);
            } else
            {
                // Player is not carrying anything
            }
        }
        else
        {
            // There is a PetShopObject
            if (player.HasPetShopObject())
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything
                GetPetShopObject().SetPetShopObjectParent(player);
            }
        }
    }
}
