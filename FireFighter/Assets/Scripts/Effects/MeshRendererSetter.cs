using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererSetter : MonoBehaviour
{
    [SerializeField] private MeshRenderer render;

    private Material[] defaultMaterials;


    private void Start()
    {
        defaultMaterials = render.materials;
    }

    public void SetMaterial(Material value)
    {
        Material[] m = new Material[render.materials.Length];
        for (int i = 0; i < m.Length; i++)
        {
            m[i] = value;
        }
        render.materials = m;
    }

    public void SetMaterialsToDefault()
    {
        render.materials = defaultMaterials;
    }
}
