using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pets_Build : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] bodyMesh;
    [SerializeField] private Texture petTextureColor;

    private void Start()
    {
        foreach (MeshRenderer mr in bodyMesh)
        {
            mr.material.mainTexture = petTextureColor;
        }
    }
}
