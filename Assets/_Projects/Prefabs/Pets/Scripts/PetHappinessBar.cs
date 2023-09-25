using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetHappinessBar : MonoBehaviour
{
    private Slider happinessBar;
    

    // Start is called before the first frame update
    void Start()
    {
        happinessBar = GetComponentInChildren<Slider>();
    }

    public void UpdateHappinessBar(float currentValue, float maxValue)
    {
        happinessBar.value = currentValue / maxValue;
    }
}
