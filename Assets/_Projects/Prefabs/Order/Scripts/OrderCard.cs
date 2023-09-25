using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderCard : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Image petIcon;
    [SerializeField] private Transform taskParent;
    [SerializeField] private OrderTask prefOrderTask;
    public int orderCardIndex;

    [Header("Properties")] [HideInInspector]
    public List<OrderTask> taskList;
    public OrderCardObjectSO[] taskListSO;

    public void SetIconCard(Sprite _sprite)
    {
        petIcon.sprite = _sprite;
    }

    public void SetupCard(OrderCardObjectSO[] _taskList)
    {
        taskListSO = _taskList;

        if (_taskList != null)
        {
            foreach (OrderCardObjectSO oc in _taskList)
            {
                OrderTask ot = Instantiate(prefOrderTask, taskParent);
                ot.Setup(oc.sorted, oc.icon);
                taskList.Add(ot);
            }
        }
    }

    public void Setup()
    {
        
    }
}
