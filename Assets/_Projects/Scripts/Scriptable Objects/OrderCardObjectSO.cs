using UnityEngine;

[CreateAssetMenu]
public class OrderCardObjectSO : ScriptableObject
{
    public Sprite icon;
    public string taskName;
    public OrderType orderType;
    public OrderTaskCategory taskType;
    public bool sorted = true;
    public bool completed = false;
}


