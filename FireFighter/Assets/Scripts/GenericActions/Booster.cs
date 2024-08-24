using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float force;
    [SerializeField] private float maxSpeed = float.PositiveInfinity;

    [SerializeField] private GameObject target;
    [SerializeField] private bool active = true;
    [SerializeField] private float lastVelocity;

    public void SetActive(bool value)
    {
        active = value;
    }

    private void FixedUpdate()
    {
        Push();
    }

    private void Push()
    {
        if (active)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            float velocity = Vector3.Dot(dir, rb.velocity);
            lastVelocity = velocity;
            if (velocity >= maxSpeed)
            {
                return;
            }

            Vector3 boost = force * dir;
            rb.AddForce(boost, ForceMode.Acceleration);
        }
    }
}
