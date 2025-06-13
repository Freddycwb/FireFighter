using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocitySetter : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public void SetVelocityToZero()
    {
        rb.linearVelocity = Vector3.zero;
    }
}
