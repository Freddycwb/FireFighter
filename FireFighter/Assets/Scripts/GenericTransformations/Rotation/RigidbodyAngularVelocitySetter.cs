using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyAngularVelocitySetter : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public void SetAngularVelocityToZero()
    {
        rb.angularVelocity = Vector3.zero;
    }
}
