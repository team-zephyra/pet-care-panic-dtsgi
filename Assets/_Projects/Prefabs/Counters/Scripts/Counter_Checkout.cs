using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Checkout : Counter
{
    [SerializeField] private PetObjectSO petObjectSO;

    public override void Interact(PlayerInteraction player)
    {
        
        if (!HasPetObject() && player.HasPetObject())
        {
            // Can put "something" to Counter
            player.GetPetObject().SetPetObjectParent(this);

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
        // Do Checkout Logic
        

        // Playing Pet Animations
        yield return new WaitForSeconds(1f);
        GetPetObject().CheckoutPet();
    }
}
