using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedCounterVisual;

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
        if (e.selectedCounter == clearCounter)
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
        selectedCounterVisual.SetActive(true);
    }

    private void Hide()
    {
        selectedCounterVisual?.SetActive(false);
    }
}
