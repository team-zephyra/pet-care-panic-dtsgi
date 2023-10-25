using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CS_Bathing_Drying : Counter, ICounterServices
{

    #region Services

    public override void ServiceFinished()
    {
        canTakePet = true;

        // Do Order Checklist Here!
        //Debug.Log(currentPet.CheckNeedsCaategory() + " vs " + taskCategory);
        if (petObject.CheckNeedsCategory() == this.taskCategory)
        {
            GameManager.instance.UpdateOrderTask(OrderTaskCategory.Drying, petObject.pet_order_index);
            petObject.UpdateScore(orderType);
        }

        // Do End VFX here
        VFX.gameObject.SetActive(false);
        ActiveBubbleEffect(BubbleType.finish);
        CounterSFX.PlayOneShot(SfxType.Bubble);

        // Restart DecreaseHappiness for the current pet if one exists.
        if (petObject != null)
        {
            petObject.StartDecreaseHappiness();
        }
    }

    public override void ServiceStarting()
    {
        // Store currentPet value from GetPetObject
        if (petObject == null)
        {
            petObject = GetPetObject();
        }

        // Initiate starting services
        StartCoroutine(ServiceOnProgress());

        // Do Start VFX here
        VFX.gameObject.SetActive(true);
        CounterSFX.PlayOneShot(SfxType.Progress);

        petObject.IsOnServices = true;
    }

    public IEnumerator ServiceOnProgress()
    {
        // Playing Pet Animations

        // Do Services Logic
        float normalizeTime = 0;
        
        while (normalizeTime < 1f)
        {
            normalizeTime += Time.deltaTime / duration;
            if(normalizeTime >= 1)
            {
                normalizeTime = 1;
            }

            SetImageProgress(normalizeTime);
            yield return null;
        }

        yield return null;
        ServiceFinished();
    }

    private void SetImageProgress(float fillAmount)
    {
        fillAmount= Mathf.Clamp01(fillAmount);

        var newScale = this.progressBarFill.localScale;
        newScale.x = fillAmount;
        this.progressBarFill.localScale = newScale;
    }

    #endregion
}
