using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterEvents : MonoBehaviour
{
    [SerializeField] private InvokeAfter invokeAfter;

    [SerializeField] private UnityEvent onActionCall;
    [SerializeField] private UnityEvent onSubActionCall;

    private bool listening;

    private void OnEnable()
    {
        if (invokeAfter != null && !listening)
        {
            invokeAfter.onActionCall += OnActionCall;
            invokeAfter.onSubActionCall += OnSubActionCall;
            listening = true;
        }
    }

    void OnActionCall()
    {
        if (enabled)
        {
            onActionCall.Invoke();
        }
    }

    void OnSubActionCall()
    {
        onSubActionCall.Invoke();
    }

    private void OnDisable()
    {
        if (invokeAfter != null && listening)
        {
            invokeAfter.onActionCall -= OnActionCall;
            invokeAfter.onSubActionCall -= OnSubActionCall;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
