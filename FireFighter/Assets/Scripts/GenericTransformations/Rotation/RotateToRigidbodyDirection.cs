using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Windows;

public class RotateToRigidbodyDirection : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float rotateVel;
    [SerializeField] private float offset;

    private Quaternion target;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (rb.linearVelocity.normalized == Vector3.zero)
        {
            return;
        }
        transform.rotation = Quaternion.LookRotation(rb.linearVelocity.normalized);
    }
}
