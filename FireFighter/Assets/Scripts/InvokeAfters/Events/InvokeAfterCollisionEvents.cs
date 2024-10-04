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
