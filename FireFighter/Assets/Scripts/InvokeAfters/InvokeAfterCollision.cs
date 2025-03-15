using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterCollision : InvokeAfter
{

    [SerializeField] private List<string> tags = new List<string>();
    [SerializeField] private bool ignoreTags;

    [System.Flags]
    public enum Types
    {
        None = 0,
        trigger = 1,
        collision = 2,
    }

    [SerializeField] private Types collisionTypes = Types.trigger | Types.collision;


    public GameObject lastCollision { get; private set; }
    public Vector3 lastCollisionPoint { get; private set; }

    private List<GameObject> collisions = new List<GameObject>();
    private List<GameObject> collisionsThisFrame = new List<GameObject>();
    private int numberOfCollisions;

    public Action<GameObject> onImpact;
    public Action<GameObject> onLeave;

    private Coroutine refreshRoutine;

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
        bool acceptTags = (tags.Contains(other.tag) && !ignoreTags) || (!tags.Contains(other.tag) && ignoreTags);
        if (tags.Count == 0 || acceptTags)
        {
            if (collisionsThisFrame.Contains(other.gameObject))
            {
                return;
            }
            lastCollision = other.gameObject;
            lastCollisionPoint = other.ClosestPoint(transform.position);
            if (onImpact != null)
            {
                onImpact.Invoke(lastCollision);
            }
            numberOfCollisions++;
            CallAction();
            if (!collisions.Contains(lastCollision))
            {
                collisions.Add(lastCollision);
            }
            collisionsThisFrame.Add(lastCollision);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!((collisionTypes & Types.trigger) != 0 && other.GetComponent<Collider>().isTrigger) && !((collisionTypes & Types.collision) != 0 && !other.GetComponent<Collider>().isTrigger))
        {
            return;
        }
        bool acceptTags = (tags.Contains(other.tag) && !ignoreTags) || (!tags.Contains(other.tag) && ignoreTags);
        if (tags.Count == 0 || acceptTags)
        {
            if (onLeave != null)
            {
                onLeave.Invoke(other.gameObject);
            }
            numberOfCollisions--;
            CallSubAction();
            if (collisions.Contains(other.gameObject))
            {
                collisions.Remove(other.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!((collisionTypes & Types.trigger) != 0 && other.collider.isTrigger) && !((collisionTypes & Types.collision) != 0 && !other.collider.isTrigger))
        {
            return;
        }
        bool acceptTags = (tags.Contains(other.gameObject.tag) && !ignoreTags) || (!tags.Contains(other.gameObject.tag) && ignoreTags);
        if (tags.Count == 0 || tags.Contains(other.gameObject.tag))
        {
            if (collisionsThisFrame.Contains(other.gameObject))
            {
                return;
            }
            lastCollision = other.gameObject;
            lastCollisionPoint = other.contacts[0].point;
            if (onImpact != null)
            {
                onImpact.Invoke(lastCollision);
            }
            numberOfCollisions++;
            CallAction();
            if (!collisions.Contains(lastCollision))
            {
                collisions.Add(lastCollision);
            }
            collisionsThisFrame.Add(lastCollision);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!((collisionTypes & Types.trigger) != 0 && other.collider.isTrigger) && !((collisionTypes & Types.collision) != 0 && !other.collider.isTrigger))
        {
            return;
        }
        bool acceptTags = (tags.Contains(other.gameObject.tag) && !ignoreTags) || (!tags.Contains(other.gameObject.tag) && ignoreTags);
        if (tags.Count == 0 || tags.Contains(other.gameObject.tag))
        {
            if (onLeave != null)
            {
                onLeave.Invoke(other.gameObject);
            }
            numberOfCollisions--;
            CallSubAction();
            if (collisions.Contains(other.gameObject))
            {
                collisions.Remove(other.gameObject);
            }
        }
    }

    private void LateUpdate()
    {
        collisionsThisFrame.Clear();
    }

    public void RemoveCollision(GameObject value)
    {
        if (collisions.Remove(value))
        {
            if (onLeave != null)
            {
                onLeave.Invoke(value);
            }
            numberOfCollisions = collisions.Count;
            CallSubAction();
        }
    }
}
