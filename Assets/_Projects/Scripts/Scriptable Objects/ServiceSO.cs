using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ServiceSO : ScriptableObject
{
    public string serviceName;
    public List<ServiceTaskSO> serviceTasks;
}
