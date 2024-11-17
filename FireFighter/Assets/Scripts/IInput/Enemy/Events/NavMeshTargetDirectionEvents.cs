using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NavMeshTargetDirectionEvents : MonoBehaviour
{
    [SerializeField] private NavMeshTargetDirection navMeshTargetDirection;

    [SerializeField] private UnityEvent onGetAwayFromTarget;
    [SerializeField] private UnityEvent onReachTarget;

    private bool listening;

    private void OnEnable()
    {
        if (navMeshTargetDirection != null)
        {
            navMeshTargetDirection.onGetAwayFromTarget += OnGetAwayFromTarget;
            navMeshTargetDirection.onReachTarget += OnReachTarget;
            listening = true;
        }
    }

    void OnGetAwayFromTarget()
    {
        if (enabled)
        {
            onGetAwayFromTarget.Invoke();
        }
    }

    void OnReachTarget()
    {
        if (enabled)
        {
            onReachTarget.Invoke();
        }
    }

    private void OnDisable()
    {
        if (navMeshTargetDirection != null && listening)
        {
            navMeshTargetDirection.onGetAwayFromTarget -= OnGetAwayFromTarget;
            navMeshTargetDirection.onReachTarget -= OnReachTarget;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
