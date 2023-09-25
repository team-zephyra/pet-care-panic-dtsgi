using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Base : Counter
{
    [SerializeField]private Pet currentPet;

    public override void Interact(PlayerInteraction _player)
    {
        if (!HasPetObject())
        {
            // There is no PetObject
            if (_player.HasPetObject())
            {
                
                // Player is carrying something
                _player.GetPetObject().SetPetObjectParent(this);
                CounterSFX.PlayOneShot(SfxType.Put);

                // Store currentPet value from GetPetObject
                //if (currentPet == null)
                //{
                //    currentPet = GetPetObject();
                //    currentPet.StartDecreaseHappiness();    
                //}
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
                CounterSFX.PlayOneShot(SfxType.Take);
            }
        }
    }
}
