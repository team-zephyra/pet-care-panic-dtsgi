using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Checkout : Counter
{
    [SerializeField] private PetObjectSO petObjectSO;

    public override void Interact(PlayerInteraction _player)
    {
        
        if (!HasPetObject() && _player.HasPetObject())
        {
            // Can put "something" to Counter
            _player.GetPetObject().SetPetObjectParent(this);

            CounterSFX.PlayPut();
            // Checkout Pet and completing order
            StartCoroutine(CheckoutPet());
        }
        else
        {
            // Do Audio Effect "Cannot Checkout"
            Debug.Log("Cannot Checkout");
        }
    }

    public IEnumerator CheckoutPet()
    {
        // Playing Pet Animations

        // Do Checkout Logic
        yield return new WaitForSeconds(0.5f);
        CounterSFX.PlaySFX();

        
        yield return new WaitForSeconds(0.5f);
        GetPetObject().CheckoutPet();
    }
}
