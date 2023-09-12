using UnityEngine;

[CreateAssetMenu]
public class OrderCardObjectSO : ScriptableObject
{
    public Sprite icon;
    public string taskName;
    public OrderType taskType;
    public bool sorted = true;
}


