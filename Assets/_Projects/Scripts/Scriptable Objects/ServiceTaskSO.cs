using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Service Task")]
public class ServiceTaskSO : ScriptableObject
{
    public string taskName;
    public bool isCompleted;
}
