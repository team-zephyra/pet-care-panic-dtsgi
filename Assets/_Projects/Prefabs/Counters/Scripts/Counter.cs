using UnityEngine;

[RequireComponent(typeof(CounterAudio))]
public class Counter : MonoBehaviour, IPetObjectParent
{
    [SerializeField] private Transform counterSurfacePosition;
    private Pet petObject;
    private CounterAudio sfx;

    private void Awake()
    {
        if (!CounterSFX)
        {
            sfx = GetComponent<CounterAudio>();
        }
    }

    public virtual void Interact(PlayerInteraction playerInteraction)
    {
        // Child classes should override this method to implement their own logic
    }

    public Transform GetSurfacePosition() { return counterSurfacePosition; }

    public void SetPetObject(Pet _petObject){ this.petObject = _petObject; }

    public Pet GetPetObject() { return petObject; }

    public void ClearPetObject() {  petObject = null; }

    public bool HasPetObject() { return petObject != null; }

    protected CounterAudio CounterSFX { get { return sfx; } }
}
