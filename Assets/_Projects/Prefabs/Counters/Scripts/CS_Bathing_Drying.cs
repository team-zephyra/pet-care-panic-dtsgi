using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CS_Bathing_Drying : Counter, ICounterServices
{
    [Header("Pet Settings")]
    [SerializeField] private PetObjectSO petObjectSO;
    private Pet currentPet;

    [Header("Counter Setup")]
    [SerializeField]private float duration = 7;
    [SerializeField]private Transform progressBarFill;
    [SerializeField]private Transform VFX;
    private bool canTakePet = true;

    private void Start()
    {

    }

    public override void Interact(PlayerInteraction _player)
    {
        if (!HasPetObject() && _player.HasPetObject())
        {
            CounterSFX.PlayOneShot(SfxType.Put);
            ActiveBubbleEffect(BubbleType.counter);
            PetRegister(_player);
        }
        
        if(HasPetObject() && canTakePet)
        {
            CounterSFX.PlayOneShot(SfxType.Take);
            DeactiveBubbleEffect();
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

    public void ServiceFinished()
    {
        canTakePet = true;

        // Do End VFX here
        ActiveBubbleEffect(BubbleType.finish);
        CounterSFX.PlayOneShot(SfxType.Bubble);
        VFX.gameObject.SetActive(false);

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
        CounterSFX.PlaySFX();
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

    public void PetRegister(PlayerInteraction player)
    {
        if (!HasPetObject() && player.HasPetObject())
        {
            // Can put "something" to Counter
            player.GetPetObject().SetPetObjectParent(this);
            canTakePet = false;

            // Service On Progress
            ServiceStarting();
        }
        else
        {
            // Do Audio Effect "Cannot Put Any PET"
            Debug.Log("Cannot Put Any PET");
        }
    }

    public void PetUnregister(PlayerInteraction player)
    {
        if (HasPetObject() && !player.HasPetObject()) 
        {
            // Reset currentPet value
            PetTakenFromCounter();

            //DO Order Checklist Here!
            GetPetObject().SetPetObjectParent(player);

            // Clear Counter
            SetImageProgress(0f);
            canTakePet = false;
        }
    }
}
