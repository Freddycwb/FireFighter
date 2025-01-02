using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelShadingSetProperties : MonoBehaviour
{
    [SerializeField] private Vector2 tiling;
    [SerializeField] private Vector2 offset;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propertyBlock);

        propertyBlock.SetVector("_Offset", offset);
        propertyBlock.SetVector("_Tiling", tiling);
        renderer.SetPropertyBlock(propertyBlock);
    }
}
