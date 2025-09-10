using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InvokeAfterFloat : InvokeAfter
{
    [SerializeField] private UnityEvent<float> actionFloat;
    [SerializeField] private UnityEvent<float> subActionFloat;

    public Action onActionCallFloat;
    public Action onSubActionCallFloat;

    public void CallActionFloat(float value)
    {
        actionFloat.Invoke(value);
        if (onActionCallFloat != null && gameObject.activeSelf)
        {
            onActionCallFloat.Invoke();
        }
    }

    public void CallSubActionFloat(float value)
    {
        subActionFloat.Invoke(value);
        if (onSubActionCallFloat != null && gameObject.activeSelf)
        {
            onSubActionCallFloat.Invoke();
        }
    }
}
