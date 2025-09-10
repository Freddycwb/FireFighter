using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGroupFloatEvent : MonoBehaviour
{
    [SerializeField] private List<InvokeAfterFloat> invokeAfters = new List<InvokeAfterFloat>();

    public void CallActions()
    {
        foreach (InvokeAfter invokeAfter in invokeAfters)
        {
            invokeAfter.CallAction();
        }
    }

    public void CallSubActions()
    {
        foreach (InvokeAfter invokeAfter in invokeAfters)
        {
            invokeAfter.CallSubAction();
        }
    }

    public void CallFloatActions(float value)
    {
        foreach (InvokeAfterFloat invokeAfter in invokeAfters)
        {
            invokeAfter.CallActionFloat(value);
        }
    }

    public void CallFloatSubActions(float value)
    {
        foreach (InvokeAfterFloat invokeAfter in invokeAfters)
        {
            invokeAfter.CallSubActionFloat(value);
        }
    }

    public void AddInvokeAfter(InvokeAfterFloat value)
    {
        if (value != null)
        {
            invokeAfters.Add(value);
        }
    }

    public void AddInvokeAfter(GameObject value)
    {
        AddInvokeAfter(value.GetComponent<InvokeAfterFloat>());
    }

    public void RemoveAllInvokeAfters()
    {
        invokeAfters.Clear();
    }
}
