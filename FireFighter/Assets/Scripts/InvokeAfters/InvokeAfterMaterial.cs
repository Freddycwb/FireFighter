using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InvokeAfterMaterial : InvokeAfter
{
    [SerializeField] private UnityEvent<Material> actionMaterial;
    [SerializeField] private UnityEvent<Material> subActionMaterial;

    public Action onActionCallMaterial;
    public Action onSubActionCallMaterial;

    public void CallActionMaterial(Material value)
    {
        actionMaterial.Invoke(value);
        if (onActionCallMaterial != null && gameObject.activeSelf)
        {
            onActionCallMaterial.Invoke();
        }
    }

    public void CallSubActionMaterial(Material value)
    {
        subActionMaterial.Invoke(value);
        if (onSubActionCallMaterial != null && gameObject.activeSelf)
        {
            onSubActionCallMaterial.Invoke();
        }
    }
}
