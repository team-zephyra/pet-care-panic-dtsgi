using UnityEngine;
using UnityEngine.UI;

public class OrderTask : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform completeImage;
    [SerializeField] private Image icon;
    private bool sorted;

    [Header("Properties")]
    [SerializeField] private Sprite taskImage;

    public void Setup(bool sorted, Sprite taskImage)
    {
        this.sorted = sorted;
        this.taskImage = taskImage;

        icon.sprite= taskImage;
    }

    public void TaskClear()
    {
        completeImage.gameObject.SetActive(true);
    }
}
