using System;
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

    public Action onActive;
    public Action onInactive;

    public void SetTarget(GameObject value)
    {
        target = value;
    }

    public void SetActive(bool value)
    {
        if (active == value) 
        {
            return;
        }
        active = value;

        if (active)
        {
            if (onActive != null)
            {
                onActive.Invoke();
            }
        }
        else
        {
            if (onInactive != null)
            {
                onInactive.Invoke();
            }
        }
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
            float velocity = Vector3.Dot(dir, rb.linearVelocity);
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
