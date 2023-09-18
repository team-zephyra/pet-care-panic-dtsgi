using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetHappinessBar : MonoBehaviour
{
    private Slider happinessBar;
    private Camera mainCamera;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    

    // Start is called before the first frame update
    void Start()
    {
        happinessBar = GetComponentInChildren<Slider>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
        transform.position = target.position + offset;
    }

    public void UpdateHappinessBar(float currentValue, float maxValue)
    {
        happinessBar.value = currentValue / maxValue;
    }
}
