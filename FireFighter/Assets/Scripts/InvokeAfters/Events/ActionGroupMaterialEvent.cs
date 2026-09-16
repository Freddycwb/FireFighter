using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGroupMaterialEvent : MonoBehaviour
{
    [SerializeField] private List<InvokeAfterMaterial> invokeAfters = new List<InvokeAfterMaterial>();

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

    public void CallMaterialActions(Material value)
    {
        foreach (InvokeAfterMaterial invokeAfter in invokeAfters)
        {
            invokeAfter.CallActionMaterial(value);
        }
    }

    public void CallMaterialSubActions(Material value)
    {
        foreach (InvokeAfterMaterial invokeAfter in invokeAfters)
        {
            invokeAfter.CallActionMaterial(value);
        }
    }

    public void CallActions(Material value)
    {
        foreach (InvokeAfterMaterial invokeAfter in invokeAfters)
        {
            invokeAfter.CallActionMaterial(value);
        }
    }

    public void CallSubActions(Material value)
    {
        foreach (InvokeAfterMaterial invokeAfter in invokeAfters)
        {
            invokeAfter.CallActionMaterial(value);
        }
    }

    public void AddInvokeAfter(InvokeAfterMaterial value)
    {
        if (value != null)
        {
            invokeAfters.Add(value);
        }
    }

    public void AddInvokeAfter(GameObject value)
    {
        AddInvokeAfter(value.GetComponent<InvokeAfterMaterial>());
    }
}
