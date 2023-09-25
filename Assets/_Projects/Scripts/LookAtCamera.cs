using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        mainCamera = Camera.main;
        if(target == null)
        {
            target = transform;
        }
        transform.rotation = mainCamera.transform.rotation;
        transform.position = target.position + offset;
    }
}
