using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CS_Bathing_Drying : Counter, ICounterServices
{
    [Header("Pet Settings")]

    [Header("Counter Setup")]
    [SerializeField] private OrderType orderType;
    [SerializeField]private OrderTaskCategory taskCategory;
    [SerializeField]private float duration = 7;
    [SerializeField]private Transform progressBarFill;
    [SerializeField]private Transform VFX;
    private bool canTakePet = true;

    private Pet currentPet;

    private void Start()
    {

    }

    #region Interactions

    public override void Interact(PlayerInteraction _player)
    {
        if (!HasPetObject() && _player.HasPetObject())
        {
            PetRegister(_player);
        }
        
        if(HasPetObject() && canTakePet)
        {
            PetUnregister(_player);
        }
    }

    private void PetTakenFromCounter()
    {
        if (currentPet != null)
        {
            currentPet.StopDecreaseHappiness();
            currentPet = null;
        }
    }

    public void PetRegister(PlayerInteraction _player)
    {

        CounterSFX.PlayOneShot(SfxType.Put);
        ActiveBubbleEffect(BubbleType.objectBuble);

        _player.GetPetObject().SetPetObjectParent(this);
        canTakePet = false;

        // Refusing PET
        if (base.petObject.CheckNeedsCategory() != this.taskCategory)
        {
            StartCoroutine(RefusingPET(_player));
        }
        else
        {
            // Service On Progress
            ServiceStarting();
        }
    }

    public void PetUnregister(PlayerInteraction player)
    {

        CounterSFX.PlayOneShot(SfxType.Take);
        DeactiveBubbleEffect();

        currentPet.IsOnServices = false;
        GetPetObject().SetPetObjectParent(player);

        // Reset currentPet value
        PetTakenFromCounter();

        // Clear Counter
        SetImageProgress(0f);
        canTakePet = false;
    }

    private IEnumerator RefusingPET(PlayerInteraction _player)
    {
        // Decrease Score Here !!
        Debug.Log("This is note pet want, see the orders !!!!");
        base.petObject.PetExpression(EPetExpression.Angry);
        yield return new WaitForSeconds(0.5f);
        PetUnregister(_player);
    }
#endregion

    #region Services

    public void ServiceFinished()
    {
        canTakePet = true;

        // Do Order Checklist Here!
        //Debug.Log(currentPet.CheckNeedsCaategory() + " vs " + taskCategory);
        if (currentPet.CheckNeedsCategory() == this.taskCategory)
        {
            GameManager.instance.UpdateOrderTask(OrderTaskCategory.Drying, currentPet.pet_order_index);
            currentPet.UpdateScore(orderType);
        }

        // Do End VFX here
        VFX.gameObject.SetActive(false);
        ActiveBubbleEffect(BubbleType.finish);
        CounterSFX.PlayOneShot(SfxType.Bubble);

        // Restart DecreaseHappiness for the current pet if one exists.
        if (currentPet != null)
        {
            currentPet.StartDecreaseHappiness();
        }
    }

    public void ServiceStarting()
    {
        // Store currentPet value from GetPetObject
        if (currentPet == null)
        {
            currentPet = GetPetObject();
        }

        // Initiate starting services
        StartCoroutine(ServiceOnProgress());

        // Do Start VFX here
        VFX.gameObject.SetActive(true);
        CounterSFX.PlayOneShot(SfxType.Progress);

        currentPet.IsOnServices = true;
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
