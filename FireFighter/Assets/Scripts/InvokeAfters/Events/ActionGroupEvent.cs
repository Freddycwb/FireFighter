using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGroupEvent : MonoBehaviour
{
    [SerializeField] private List<InvokeAfter> invokeAfters = new List<InvokeAfter>();

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

    public void AddInvokeAfter(InvokeAfter value)
    {
        if (value != null)
        {
            invokeAfters.Add(value);
        }
    }

    public void AddInvokeAfter(GameObject value)
    {
        AddInvokeAfter(value.GetComponent<InvokeAfter>());
    }
}
