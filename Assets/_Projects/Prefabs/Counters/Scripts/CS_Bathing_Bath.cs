using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class CS_Bathing_Bath : Counter, ICounterServices
{

    [Header("Counter Setup")]
    [SerializeField] private OrderTaskCategory taskCategory;
    [SerializeField] private float duration = 7;
    [SerializeField] private Transform progressBarFill;
    [SerializeField] private Transform VFX;
    private bool canTakePet = true;

    #region Interactions
    public override void Interact(PlayerInteraction _player)
    {
        if (!HasPetObject() && _player.HasPetObject())
        {
            PetRegister(_player);
        }
        
        if (HasPetObject() && !_player.HasPetObject() && canTakePet)
        {
            PetUnregister(_player);
        }
    }

    private void PetTakenFromCounter()
    {
        if (petObject != null)
        {
            petObject.StopDecreaseHappiness();
            petObject = null;
        }
    }
    public void PetRegister(PlayerInteraction _player)
    {
        CounterSFX.PlayOneShot(SfxType.Put);
        ActiveBubbleEffect(BubbleType.objectBuble);

        _player.GetPetObject().SetPetObjectParent(this);
        canTakePet = false;

        // Cek Pet on Right Task
        if (petObject.CheckNeedsCategory() != this.taskCategory)
        {
            StartCoroutine(RefusingPET(_player));
        }
        else
        {
            // Service On Progress
            ServiceStarting();
        }
    }

    private IEnumerator RefusingPET(PlayerInteraction _player)
    {
        // Decrease Score Here !!
        Debug.Log("This is note pet want, see the orders !!!!");
        petObject.PetExpression(EPetExpression.Angry);
        yield return new WaitForSeconds(0.5f);
        PetUnregister(_player);
    }

    public void PetUnregister(PlayerInteraction player)
    {
        CounterSFX.PlayOneShot(SfxType.Take);
        DeactiveBubbleEffect();
 
        GetPetObject().SetPetObjectParent(player);

        // Reset currentPet value
        PetTakenFromCounter();

        // Clear Counter
        SetImageProgress(0f);
        canTakePet = false;
    }

    #endregion

    #region Services 
    public void ServiceFinished()
    {
        canTakePet = true;

        // Do Order Checklist Here!
        //Debug.Log(petObject.CheckNeedsCaategory() + " vs " + taskCategory);
        if(petObject.CheckNeedsCategory() == this.taskCategory)
        {
            // Scoring Here
            GameManager.instance.UpdateOrderTask(OrderTaskCategory.Bathing, petObject.pet_order_index);
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

    public void ServiceStarting()
    {
        // Store currentPet value from GetPetObject
        if (petObject == null)
        {
            petObject = GetPetObject();
        }

        // Initiate starting services
        StartCoroutine(ServiceOnProgress());

        // Do Start VFX here
        CounterSFX.PlayOneShot(SfxType.Progress);
        VFX.gameObject.SetActive(true);
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
