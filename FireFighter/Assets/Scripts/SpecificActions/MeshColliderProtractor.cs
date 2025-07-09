using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColliderProtractor : MonoBehaviour
{
    [SerializeField] private MeshCollider receiveMesh;
    [SerializeField] private MeshCollider meshCollider;

    public void Transfer()
    {
        receiveMesh.sharedMesh = meshCollider.sharedMesh;
    }
}
