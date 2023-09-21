using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSetup
{
    [Tooltip("Add Customer Order")]
    public SpawnSetup[] addCustomerOrder;
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
    public PetType petType;
    
}
