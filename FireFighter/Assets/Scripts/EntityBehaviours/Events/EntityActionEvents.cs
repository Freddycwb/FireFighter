using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityActionEvents : MonoBehaviour
{
    [SerializeField] private EntityAction entityAction;

    [SerializeField] private UnityEvent onActionTrigger;

    private bool listening;

    private void OnEnable()
    {
        if (entityAction != null)
        {
            entityAction.onActionTrigger += OnActionTrigger;
            listening = true;
        }
    }

    void OnActionTrigger()
    {
        onActionTrigger.Invoke();
    }

    private void OnDisable()
    {
        if (entityAction != null && listening)
        {
            entityAction.onActionTrigger -= OnActionTrigger;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
