using UnityEngine;

public class Counter_Checkin : Counter
{
    [SerializeField] 
    private PetObjectSO petObjectSO;
    private Pet currentPet;

    private void Start()
    {
        // For Debuging PET Handle
        //CheckInPet(petObjectSO);
    }

    public override void Interact(PlayerInteraction _player)
    {
        if (HasPetObject() && !_player.HasPetObject())
        {
            // Counter have a PetObject and Player is not carrying anything
            // Give PetObject to Player

            currentPet.isOnCheckInCounter = false;

            PetTakenFromCounter();

            GetPetObject().SetPetObjectParent(_player);
            CounterSFX.PlayOneShot(SfxType.Take);
        }
        //else if (!HasPetObject() && _player.HasPetObject())
        //{
        //    // Counter does not have a PetObject and Player is carrying something
        //    // Can put "something" to Counter
        //    _player.GetPetObject().SetPetObjectParent(this);
        //    CounterSFX.PlayOneShot(SfxType.Put);
        //}
    }

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

            currentPet = pet;
        }

        return currentPet;
    }

    private void PetTakenFromCounter()
    {
        if (currentPet != null)
        {
            currentPet.StopDecreaseHappiness();
            currentPet = null;
        }
    }
}
