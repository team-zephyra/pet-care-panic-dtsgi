using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Checkout : Counter
{
    public override void Interact(PlayerInteraction _player)
    {
        
        if (!HasPetObject() && _player.HasPetObject())
        {
            // Can put "something" to Counter
            _player.GetPetObject().SetPetObjectParent(this);

            CounterSFX.PlayOneShot(SfxType.Put);

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
        // This seconds should be a little bit higher than Pet Happy animation
        float seconds = 3f;

        // Trigger Happy Animation
        GetPetObject().GetComponent<PetAnimation>().TriggerHappyAnimation();

        // Do Checkout Logic
        yield return new WaitForSeconds(seconds);
        CounterSFX.PlayOneShot(SfxType.Progress);


        yield return new WaitForSeconds(0.5f);
        GetPetObject().CheckoutPet();
    }
}
