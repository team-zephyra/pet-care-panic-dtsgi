using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Setup Gameplay")]
    public GameSetup gameSetup;
    
    [SerializeField] private Orders orderList;
    
    private List<Pet> petList;


}
