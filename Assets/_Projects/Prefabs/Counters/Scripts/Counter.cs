using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(CounterAudio))]
public class Counter : MonoBehaviour, IPetObjectParent
{
    [Header("Basic Setup")]
    [SerializeField] private Transform counterSurfacePosition;
    [SerializeField] private BubbleEffect bubbleEffect;
    protected Pet petObject;
    protected CounterAudio sfx;
    [SerializeField]protected bool canTakePet = true;
    [SerializeField]protected bool canPutPet = true;

    [Header("Counter Setup")]
    [SerializeField] protected OrderType orderType;
    [SerializeField] protected OrderTaskCategory taskCategory;
    [SerializeField] protected float duration = 7;
    [SerializeField] protected Transform progressBarFill;
    [SerializeField] protected Transform VFX;

    private void Awake()
    {
        if (!CounterSFX)
        {
            sfx = GetComponent<CounterAudio>();
        }
    }

    public void Interact(PlayerInteraction _player)
    {
        if (!HasPetObject() && _player.HasPetObject() && canPutPet)
        {
            PetRegister(_player);
        }

        if (HasPetObject() && !_player.HasPetObject() && canTakePet)
        {
            PetUnregister(_player);
        }
    }

    public Transform GetSurfacePosition() { return counterSurfacePosition; }

    public void SetPetObject(Pet _petObject) { this.petObject = _petObject; }

    public Pet GetPetObject() { return petObject; }

    public void ClearPetObject() { petObject = null; }

    public bool HasPetObject() { return petObject != null; }

    protected CounterAudio CounterSFX { get { return sfx; } }

    protected virtual void ActiveBubbleEffect(BubbleType _type) { if (bubbleEffect) bubbleEffect.Enable(_type); }

    protected virtual void DeactiveBubbleEffect() { if(bubbleEffect) bubbleEffect.Disable(); }

    #region Basic Integrastion

    public void PetRegister(PlayerInteraction _player)
    {
        CounterSFX.PlayOneShot(SfxType.Put);
        ActiveBubbleEffect(BubbleType.objectBuble);

        _player.GetPetObject().SetPetObjectParent(this);
        
        canTakePet = false;
        Debug.Log("Task Categroy "+taskCategory.ToString());
        // Cek Pet on Right Task
        if(taskCategory == OrderTaskCategory.Checkout)
        {
            ServiceStarting();
        }
        else if (petObject.CheckNeedsCategory() != taskCategory)
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

        GetPetObject().IsOnServices = false;
        GetPetObject().SetPetObjectParent(player);

        // Reset currentPet value
        PetTakenFromCounter();

        // Clear Counter
        //SetImageProgress(0f);
        canTakePet = false;
    }

    private IEnumerator RefusingPET(PlayerInteraction _player)
    {
        // Decrease Score Here !!
        Debug.Log("This is note pet want, see the orders !!!!");
        petObject.PetExpression(EPetExpression.Angry);
        yield return new WaitForSeconds(0.5f);
        PetUnregister(_player);
    }

    private void PetTakenFromCounter()
    {
        if (GetPetObject() != null)
        {
            GetPetObject().StopDecreaseHappiness();
            petObject = null;
        }
    }

    #endregion

    public virtual void ServiceStarting() {
        // Child classes should override this method to implement their own logic
    }

    public virtual void ServiceFinished() {
        // Child classes should override this method to implement their own logic
    }

    public virtual void ServiceReset() {
        // Child classes should override this method to implement their own logic 
    }
}
