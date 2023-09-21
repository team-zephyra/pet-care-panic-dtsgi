using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCard : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private SpriteRenderer petIcon;
    [SerializeField] private Transform taskParent;
    [SerializeField] private OrderTask prefOrderTask;

    [Header("Properties")]
    private OrderCardObjectSO[] taskList;

    public void NewOrderCard(OrderCardObjectSO[] taskList)
    {
        this.taskList = taskList;
        SetupTask();
    }

    public void SetupTask()
    {
        foreach(OrderCardObjectSO oc in taskList)
        {
            OrderTask ot = Instantiate(prefOrderTask, taskParent);
            ot.Setup(oc.sorted, oc.icon);
        }
    }
}
