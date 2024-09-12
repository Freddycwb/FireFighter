using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterCollisionEvents : MonoBehaviour
{
    [SerializeField] private InvokeAfterCollision invokeAfterCollision;

    [SerializeField] private UnityEvent<GameObject> onImpact;
    [SerializeField] private UnityEvent<GameObject> onLeave;

    private bool listening;

    private void OnEnable()
    {
        if (invokeAfterCollision != null && !listening)
        {
            invokeAfterCollision.onImpact += OnImpact;
            invokeAfterCollision.onImpact += OnLeave;
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

    private void OnDisable()
    {
        if (invokeAfterCollision != null && listening)
        {
            invokeAfterCollision.onImpact -= OnImpact;
            invokeAfterCollision.onImpact -= OnLeave;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
