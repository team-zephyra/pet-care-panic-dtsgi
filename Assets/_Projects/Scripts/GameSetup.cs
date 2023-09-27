using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup: MonoBehaviour
{
    [Header("Checkin Counter Register")]
    public List<Counter_Checkin> checkinCounters;

    [Tooltip("Add Customer Order")]
    public SpawnSetup[] addCustomerOrder;

    [Header("Setup MinimumScore")]
    [SerializeField] private int MinimumScoreStar1 = 700;
    [SerializeField] private int MinimumScoreStar2 = 1000;
    [SerializeField] private int MinimumScoreStar3 = 1300;

    private void Start()
    {
        GameManager.instance.SetMinimumScore(MinimumScoreStar1, MinimumScoreStar2, MinimumScoreStar3);    
    }

    public bool IsCounterFree()
    {
        for(int i=0; i<checkinCounters.Count; i++)
        {
            if (!checkinCounters[i].HasPetObject())
            {
                return true;
                break;
            }
            if(i == checkinCounters.Count - 1)
            {
                return false;
                break;
            }
        }
        return false;
    }
}

[System.Serializable]
public class SpawnSetup
{
    [Tooltip("Its Generate sorted index for spawn setup,")]
    public int _index;
    [Tooltip("Time for new Customer Spawn")]
    public float timeToSpawn;
    [Tooltip("Input the Order Type")]
    public OrderType orderType;
    [Tooltip("Input the Order Type")]
    public PetCategory petType;

    // functions

    public PetObjectSO GetPET()
    {
        return GameManager.instance.petInitiate.GetPetsByCategory(petType);
    }
}