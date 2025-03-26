using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InvokeAfterGameObject : InvokeAfter
{
    [SerializeField] private UnityEvent<GameObject> actionGameObject;
    [SerializeField] private UnityEvent<GameObject> subActionGameObject;

    public Action onActionCallGameObject;
    public Action onSubActionCallGameObject;

    public void CallActionGameObject(GameObject value)
    {
        actionGameObject.Invoke(value);
        if (onActionCallGameObject != null && gameObject.activeSelf)
        {
            onActionCallGameObject.Invoke();
        }
    }

    public void CallSubActionGameObject(GameObject value)
    {
        subActionGameObject.Invoke(value);
        if (onSubActionCallGameObject != null && gameObject.activeSelf)
        {
            onSubActionCallGameObject.Invoke();
        }
    }
}
