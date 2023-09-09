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

    public override void Interact(PlayerInteraction _player)
    {
        
        if (HasPetObject() && !_player.HasPetObject())
        {
            // Counter have a PetObject and Player is not carrying anything
            // Give PetShopObject to Player
            GetPetObject().SetPetObjectParent(_player);
            CounterSFX.PlayTake();
        }
        else if (!HasPetObject() && _player.HasPetObject())
        {
            // Counter does not have a PetObject and Player is carrying something
            // Can put "something" to Counter
            _player.GetPetObject().SetPetObjectParent(this);
            CounterSFX.PlayPut();
        }
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
