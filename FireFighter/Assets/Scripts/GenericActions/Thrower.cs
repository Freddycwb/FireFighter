using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    [SerializeField] private Transform target;

    public void Throw(GameObject value)
    {
        Rigidbody rb = null;
        if (value.GetComponent<Rigidbody>() == null)
        {
            return;
        }
        else
        {
            rb = value.GetComponent<Rigidbody>();
        }
        rb.velocity = Vector3.zero;

        Vector3 dirToObject = target.position - value.transform.position;
        float throwForce = force.x >= force.y ? force.x : Random.Range(force.x, force.y);
        rb.AddForce(dirToObject.normalized * throwForce, ForceMode.Impulse);
    }
}
