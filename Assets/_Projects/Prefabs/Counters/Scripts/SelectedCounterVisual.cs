using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private Counter baseCounter;
    [SerializeField] private GameObject[] selectedCounterVisuals;

    private bool isInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        if (isInitialized)
        {
            PlayerInteraction.Instance.OnSelectedCounterChanged += SelectedCounter;
        }
    }

    private void OnDisable()
    {
        if (isInitialized)
        {
            PlayerInteraction.Instance.OnSelectedCounterChanged -= SelectedCounter;
        }
    }

    private void Initialize()
    {
        if (PlayerInteraction.Instance != null)
        {
            PlayerInteraction.Instance.OnSelectedCounterChanged += SelectedCounter;
            isInitialized = true;
        }
        else
        {
            Debug.LogWarning("PlayerInteraction.Instance is null. Make sure PlayerInteraction is properly set up.");
        }
    }

    private void SelectedCounter(object sender, PlayerInteraction.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject selectedCounterVisual in selectedCounterVisuals)
        {
            selectedCounterVisual.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject selectedCounterVisual in selectedCounterVisuals)
        {
            selectedCounterVisual.SetActive(false);
        }
    }
}
