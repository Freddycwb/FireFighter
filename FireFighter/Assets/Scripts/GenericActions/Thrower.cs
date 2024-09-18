using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    [SerializeField] private Rigidbody throwable;
    [SerializeField] private Transform target;

    public void SetTarget(GameObject value)
    {
        target = value.transform;
    }

    public void Throw()
    {
        Throw(throwable, target);
    }

    public void Throw(GameObject value)
    {
        Rigidbody rb = null;
        Transform t = null;
        if (value.GetComponent<Rigidbody>() == null)
        {
            rb = throwable;
            t = value.transform;
        }
        else
        {
            rb = value.GetComponent<Rigidbody>();
            t = target;
        }

        if (rb == null || t == null)
        {
            return;
        }

        Throw(rb, t);
    }

    public void Throw(Rigidbody rb, Transform target)
    {
        rb.velocity = Vector3.zero;

        Vector3 dirToObject = target.position - rb.transform.position;
        float throwForce = force.x >= force.y ? force.x : Random.Range(force.x, force.y);
        rb.AddForce(dirToObject.normalized * throwForce, ForceMode.Impulse);
    }
}
