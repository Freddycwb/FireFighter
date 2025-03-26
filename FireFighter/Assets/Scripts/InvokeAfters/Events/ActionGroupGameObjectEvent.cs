using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGroupGameObjectEvent : MonoBehaviour
{
    [SerializeField] private List<InvokeAfterGameObject> invokeAfters = new List<InvokeAfterGameObject>();

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

    public void CallGameObjectActions(GameObject value)
    {
        foreach (InvokeAfterGameObject invokeAfter in invokeAfters)
        {
            invokeAfter.CallActionGameObject(value);
        }
    }

    public void CallGameObjectSubActions(GameObject value)
    {
        foreach (InvokeAfterGameObject invokeAfter in invokeAfters)
        {
            invokeAfter.CallActionGameObject(value);
        }
    }

    public void CallActions(GameObject value)
    {
        foreach (InvokeAfterGameObject invokeAfter in invokeAfters)
        {
            invokeAfter.CallActionGameObject(value);
        }
    }

    public void CallSubActions(GameObject value)
    {
        foreach (InvokeAfterGameObject invokeAfter in invokeAfters)
        {
            invokeAfter.CallActionGameObject(value);
        }
    }

    public void AddInvokeAfter(InvokeAfterGameObject value)
    {
        if (value != null)
        {
            invokeAfters.Add(value);
        }
    }

    public void AddInvokeAfter(GameObject value)
    {
        AddInvokeAfter(value.GetComponent<InvokeAfterGameObject>());
    }
}
