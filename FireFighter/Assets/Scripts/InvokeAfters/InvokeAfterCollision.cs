using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterCollision : InvokeAfter
{
    [SerializeField] private List<string> tags = new List<string>();
    public GameObject lastCollision { get; private set; }

    private int numberOfCollisions;

    public Action<GameObject> onImpact;
    public Action<GameObject> onLeave;

    public GameObject GetLastCollision()
    {
        return lastCollision;
    }

    public int GetNumberOfCollisions()
    {
        return numberOfCollisions;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tags.Count == 0 || tags.Contains(other.tag))
        {
            lastCollision = other.gameObject;
            if (onImpact != null)
            {
                onImpact.Invoke(lastCollision);
            }
            numberOfCollisions++;
            CallAction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tags.Count == 0 || tags.Contains(other.tag))
        {
            if (onLeave != null)
            {
                onLeave.Invoke(other.gameObject);
            }
            numberOfCollisions--;
            CallSubAction();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (tags.Count == 0 || tags.Contains(other.gameObject.tag))
        {
            lastCollision = other.gameObject;
            if (onImpact != null)
            {
                onImpact.Invoke(lastCollision);
            }
            numberOfCollisions++;
            CallAction();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (tags.Count == 0 || tags.Contains(other.gameObject.tag))
        {
            if (onLeave != null)
            {
                onLeave.Invoke(other.gameObject);
            }
            numberOfCollisions--;
            CallSubAction();
        }
    }
}
