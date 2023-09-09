using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Base : Counter
{
    [SerializeField] private PetObjectSO petObjectSO;

    public override void Interact(PlayerInteraction _player)
    {
        if (!HasPetObject())
        {
            // There is no PetObject
            if (_player.HasPetObject())
            {
                // Player is carrying something
                _player.GetPetObject().SetPetObjectParent(this);
            }
        }
        else
        {
            // There is a PetObject
            if (_player.HasPetObject())
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything
                GetPetObject().SetPetObjectParent(_player);
            }
        }
    }
}
