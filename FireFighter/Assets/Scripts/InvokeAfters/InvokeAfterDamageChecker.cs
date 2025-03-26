using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InvokeAfterDamageChecker : InvokeAfter
{
    [SerializeField] private UnityEvent<DamageChecker> actionDamageChecker;
    [SerializeField] private UnityEvent<DamageChecker> subActionDamageChecker;

    public Action onActionCallDamageChecker;
    public Action onSubActionCallDamageChecker;

    public void CallActionDamageChecker(DamageChecker value)
    {
        actionDamageChecker.Invoke(value);
        if (onActionCallDamageChecker != null && gameObject.activeSelf)
        {
            onActionCallDamageChecker.Invoke();
        }
    }

    public void CallSubActionDamageChecker(DamageChecker value)
    {
        subActionDamageChecker.Invoke(value);
        if (onSubActionCallDamageChecker != null && gameObject.activeSelf)
        {
            onSubActionCallDamageChecker.Invoke();
        }
    }
}
