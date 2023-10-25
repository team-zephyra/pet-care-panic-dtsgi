using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_Checkout : Counter
{

    public override void ServiceStarting()
    {
        // Initiate starting services
        StartCoroutine(CheckoutPet());
    }

    public IEnumerator CheckoutPet()
    {
        // This seconds should be a little bit higher than Pet Happy animation
        float seconds = 0.3f;

        // Trigger Happy Animation
        GetPetObject().GetComponent<PetAnimation>().TriggerHappyAnimation();

        // Do Checkout Logic
        yield return new WaitForSeconds(seconds);
        UpdateToGM();

        yield return new WaitForSeconds(0.5f);
        GetPetObject().CheckoutPet();
    }

    private void UpdateToGM()
    {
        int score = petObject.PetScore;
        int index = petObject.pet_order_index;

        if(score < 0)
        {
            score = 0;
        }

        GameManager.instance.CheckoutPet(score, index);
        CounterSFX.PlayOneShot(SfxType.Progress);
    }
}
