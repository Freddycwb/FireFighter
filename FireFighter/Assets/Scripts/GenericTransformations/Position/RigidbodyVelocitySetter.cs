using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocitySetter : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public void SetVelocityToZero()
    {
        rb.velocity = Vector3.zero;
    }
}
