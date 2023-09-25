using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PetInitiate
{
    [Tooltip("Add All of Pet here!")]
    public List<PetObjectSO> pets;

    public PetObjectSO GetPetsByCategory(PetCategory _category)
    {
        //PetObjectSO objectSO = null;

        foreach(PetObjectSO obj in pets)
        {
            if(obj.petCategory == _category) {
                return obj;
            }
        }
        Debug.Log("Finding "+_category.ToString());
        return null;
    }
}