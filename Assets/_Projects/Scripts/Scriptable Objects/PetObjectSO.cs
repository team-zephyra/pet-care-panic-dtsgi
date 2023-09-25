using UnityEngine;

[CreateAssetMenu()]
public class PetObjectSO : ScriptableObject
{
    //public Transform prefab;
    public Pet prefab;
    public string objectName;
    public Sprite petIcon;
    public PetCategory petCategory;
}
