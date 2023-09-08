using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Checkin : Counter
{
    [SerializeField] private PetObjectSO petObjectSO;

    private void Start()
    {
        // For Debuging PET Handle
        CheckInPet(petObjectSO);
    }

    public override void Interact(PlayerInteraction player)
    {
        
        if (HasPetObject() && !player.HasPetObject())
        {
            // Counter have a PetObject and Player is not carrying anything
            // Give PetShopObject to Player
            GetPetObject().SetPetObjectParent(player);
        }
        else if (!HasPetObject() && player.HasPetObject())
        {
            // Counter does not have a PetObject and Player is carrying something
            // Can put "something" to Counter
            player.GetPetObject().SetPetObjectParent(this);
        }
        //Debug Only
        /*
        else if (!HasPetObject() && !player.HasPetObject())
        {
            // Counter does not have a PetObject and Player is not carrying anything
            // Can spawn PetObject
            Transform petObjectTransform = Instantiate(petObjectSO.prefab, GetPetObjectFollowTransform());
            petObjectTransform.GetComponent<Pet>().SetPetObjectParent(this);
        }
        */
    }

    public void CheckInPet(PetObjectSO _petObjectSO)
    {
        // Can spawn PetObject

        if (_petObjectSO != null)
        {
            Transform petObjectTransform = Instantiate(petObjectSO.prefab, GetSurfacePosition());
            petObjectTransform.GetComponent<Pet>().SetPetObjectParent(this);
        }
    }
}
