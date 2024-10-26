using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterCollision : InvokeAfter
{

    [SerializeField] private List<string> tags = new List<string>();

    [System.Flags]
    public enum Types
    {
        None = 0,
        trigger = 1,
        collision = 2,
    }

    [SerializeField] private Types collisionTypes = Types.trigger | Types.collision;


    public GameObject lastCollision { get; private set; }

    private List<GameObject> collisionsThisFrame = new List<GameObject>();
    private int numberOfCollisions;

    public Action<GameObject> onImpact;
    public Action<GameObject> onLeave;

    public GameObject GetLastCollision() {
        return lastCollision;
    }

    public int GetNumberOfCollisions()
    {
        return numberOfCollisions;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!((collisionTypes & Types.trigger) != 0 && other.GetComponent<Collider>().isTrigger) && !((collisionTypes & Types.collision) != 0 && !other.GetComponent<Collider>().isTrigger))
        {
            return;
        }
        if (tags.Count == 0 || tags.Contains(other.tag))
        {
            if (collisionsThisFrame.Contains(other.gameObject))
            {
                return;
            }
            lastCollision = other.gameObject;
            if (onImpact != null)
            {
                onImpact.Invoke(lastCollision);
            }
            numberOfCollisions++;
            CallAction();
            collisionsThisFrame.Add(lastCollision);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!((collisionTypes & Types.trigger) != 0 && other.GetComponent<Collider>().isTrigger) && !((collisionTypes & Types.collision) != 0 && !other.GetComponent<Collider>().isTrigger))
        {
            return;
        }
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
        if (!((collisionTypes & Types.trigger) != 0 && other.collider.isTrigger) && !((collisionTypes & Types.collision) != 0 && !other.collider.isTrigger))
        {
            return;
        }
        if (tags.Count == 0 || tags.Contains(other.gameObject.tag))
        {
            if (collisionsThisFrame.Contains(other.gameObject))
            {
                return;
            }
            lastCollision = other.gameObject;
            if (onImpact != null)
            {
                onImpact.Invoke(lastCollision);
            }
            numberOfCollisions++;
            CallAction();
            collisionsThisFrame.Add(lastCollision);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!((collisionTypes & Types.trigger) != 0 && other.collider.isTrigger) && !((collisionTypes & Types.collision) != 0 && !other.collider.isTrigger))
        {
            return;
        }
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

    private void LateUpdate()
    {
        collisionsThisFrame.Clear();
    }
}
