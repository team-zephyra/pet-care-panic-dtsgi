using UnityEngine;

public class Counter_Checkin : Counter
{
    public Pet CheckInPet(PetObjectSO _petObjectSO)
    {
        // Can spawn PetObject
        if (_petObjectSO != null)
        {
            Transform petObjectTransform = Instantiate(_petObjectSO.prefab.transform, GetSurfacePosition());
            Pet pet = petObjectTransform.GetComponent<Pet>();
            
            pet.SetPetObjectParent(this);
            pet.isOnCheckInCounter = true;

            pet.StartDecreaseHappiness();

            petObject = pet;
            canTakePet= true;
        }

        return petObject;
    }
}
