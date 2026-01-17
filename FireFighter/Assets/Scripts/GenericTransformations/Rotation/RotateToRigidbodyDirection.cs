using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Windows;

public class RotateToRigidbodyDirection : MonoBehaviour
{
    [SerializeField] private Transform objToRotate;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float rotateVel;
    [SerializeField] private float offset;

    private Quaternion target;

    private void OnEnable()
    {
        if (objToRotate == null)
        {
            objToRotate = transform;
        }
    }

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
        objToRotate.rotation = Quaternion.LookRotation(rb.linearVelocity.normalized);
    }
}
