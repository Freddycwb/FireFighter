using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGroupDamageCheckerEvent : MonoBehaviour
{
    [SerializeField] private List<InvokeAfterDamageChecker> invokeAfters = new List<InvokeAfterDamageChecker>();

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

    public void CallDamageCheckerActions(DamageChecker value)
    {
        foreach (InvokeAfterDamageChecker invokeAfter in invokeAfters)
        {
            invokeAfter.CallActionDamageChecker(value);
        }
    }

    public void CallDamageCheckerSubActions(DamageChecker value)
    {
        foreach (InvokeAfterDamageChecker invokeAfter in invokeAfters)
        {
            invokeAfter.CallSubActionDamageChecker(value);
        }
    }

    public void AddInvokeAfter(InvokeAfterDamageChecker value)
    {
        if (value != null)
        {
            invokeAfters.Add(value);
        }
    }

    public void AddInvokeAfter(GameObject value)
    {
        AddInvokeAfter(value.GetComponent<InvokeAfterDamageChecker>());
    }
}
