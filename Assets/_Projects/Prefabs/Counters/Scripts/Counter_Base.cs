using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Base : Counter
{
    [SerializeField] private PetObjectSO petObjectSO;

    public override void Interact(PlayerInteraction player)
    {
        if (!HasPetObject())
        {
            // There is no PetObject
            if (player.HasPetObject())
            {
                // Player is carrying something
                player.GetPetObject().SetPetObjectParent(this);
            } else
            {
                // Player is not carrying anything
            }
        }
        else
        {
            // There is a PetObject
            if (player.HasPetObject())
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything
                GetPetObject().SetPetObjectParent(player);
            }
        }
    }
}
