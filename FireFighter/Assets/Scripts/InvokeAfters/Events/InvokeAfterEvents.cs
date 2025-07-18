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

    public void SetInvokeAfter(GameObjectVariable value)
    {
        SetInvokeAfter(value.Value);
    }

    public void SetInvokeAfter(GameObject value)
    {
        if (invokeAfter != null && listening)
        {
            invokeAfter.onActionCall -= OnActionCall;
            invokeAfter.onSubActionCall -= OnSubActionCall;
        }

        InvokeAfter newInvokeAfter = value.GetComponent<InvokeAfter>();

        if (newInvokeAfter != null) {
            invokeAfter = newInvokeAfter;
            invokeAfter.onActionCall += OnActionCall;
            invokeAfter.onSubActionCall += OnSubActionCall;
        }
        else
        {
            listening = false;
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
        if (enabled)
        {
            onSubActionCall.Invoke();
        }
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
