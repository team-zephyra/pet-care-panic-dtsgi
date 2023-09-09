using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CS_Bathing_Drying : Counter, ICounterServices
{
    [SerializeField] private PetObjectSO petObjectSO;

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
           CounterSFX.PlayPut();
           PetRegister(_player);
        }
        
        if(HasPetObject() && canTakePet)
        {
            CounterSFX.PlayTake();
            PetUnregister(_player);
        }
    }

    public void ServiceFinished()
    {
        canTakePet = true;

        // Do End VFX here
        VFX.gameObject.SetActive(false);
    }

    public void ServiceStarting()
    {
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
        if(HasPetObject() && !player.HasPetObject()) 
        {
            //DO Order Checklist Here!
            GetPetObject().SetPetObjectParent(player);

            // Clear Counter
            SetImageProgress(0f);
            canTakePet = false;
        }
    }
}
