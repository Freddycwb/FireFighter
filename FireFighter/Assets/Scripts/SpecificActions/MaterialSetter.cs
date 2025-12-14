using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> meshs = new List<MeshRenderer>();

    public void SetMaterial(Material value)
    {
        foreach (var mesh in meshs)
        {
            mesh.material = value;
        }
    }
}
