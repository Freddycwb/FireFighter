using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterCollisionEvents : MonoBehaviour
{
    [SerializeField] private InvokeAfterCollision invokeAfterCollision;

    [SerializeField] private UnityEvent<GameObject> onImpact;
    [SerializeField] private UnityEvent<GameObject> onLeave;
    [SerializeField] private UnityEvent<GameObject> onCallLastCollisionAction;
    [SerializeField] private UnityEvent<Vector3> onCallGetLastCollisionPoint;
    [SerializeField] private UnityEvent<Rigidbody> onCallLastRigidbodyAction;

    private bool listening;

    private void OnEnable()
    {
        if (invokeAfterCollision != null && !listening)
        {
            invokeAfterCollision.onImpact += OnImpact;
            invokeAfterCollision.onLeave += OnLeave;
            listening = true;
        }
    }

    void OnImpact(GameObject value)
    {
        if (enabled)
        {
            onImpact.Invoke(value);
        }
    }

    void OnLeave(GameObject value)
    {
        if (enabled)
        {
            onLeave.Invoke(value);
        }
    }

    public void CallLastCollisionAction()
    {
        if (enabled && invokeAfterCollision.lastCollision != null)
        {
            onCallLastCollisionAction.Invoke(invokeAfterCollision.lastCollision);
        }
    }

    public void CallGetLastCollisionPoint()
    {
        if (enabled && invokeAfterCollision.lastCollisionPoint != null)
        {
            onCallGetLastCollisionPoint.Invoke(invokeAfterCollision.lastCollisionPoint);
        }
    }

    public void CallLastRigidbodyAction()
    {
        if (enabled && invokeAfterCollision.lastRigidbody != null)
        {
            onCallLastRigidbodyAction.Invoke(invokeAfterCollision.lastRigidbody);
        }
    }

    private void OnDisable()
    {
        if (invokeAfterCollision != null && listening)
        {
            invokeAfterCollision.onImpact -= OnImpact;
            invokeAfterCollision.onLeave -= OnLeave;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
