using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterRaycast : InvokeAfter
{
    [SerializeField] private GameObject origin;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObjectVariable targetVariable;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask layer;

    public Action<Vector3> onContact;

    public void SetOrigin(GameObject value)
    {
        origin = value;
    }

    public void SetTarget(GameObject value)
    {
        target = value;
    }

    public void SetDistance(float value)
    {
        rayDistance = value;
    }

    private void CheckRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin.transform.position, (target.transform.position - origin.transform.position).normalized, out hit, rayDistance))
        {
            CallAction();
            if (onContact != null)
            {
                onContact.Invoke(hit.point);
            }
        }
        else
        {
            CallSubAction();
        }
    }
}
