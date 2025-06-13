using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterRBMove : InvokeAfter
{
    [SerializeField] private Rigidbody value;
    [SerializeField] private bool moving;
    [SerializeField] private float minVelocity;

    private void Update()
    {
        if (value.linearVelocity.magnitude > minVelocity && !moving)
        {
            moving = true;
            CallAction();
        }
        else if (value.linearVelocity.magnitude <= minVelocity && moving)
        {
            moving = false;
            CallSubAction();
        }
    }
}
