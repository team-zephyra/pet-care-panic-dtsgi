using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedCounterVisual;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += SelectedCounter;
    }

    private void SelectedCounter(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        } else
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
