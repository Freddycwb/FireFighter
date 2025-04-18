using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NavMeshTargetDirectionEvents : MonoBehaviour
{
    [SerializeField] private NavMeshTargetDirection navMeshTargetDirection;

    [SerializeField] private UnityEvent onGetAwayFromTarget;
    [SerializeField] private UnityEvent onReachTarget;
    [SerializeField] private UnityEvent onCantReachTarget;
    [SerializeField] private UnityEvent onCanReachTarget;

    private bool listening;

    private void OnEnable()
    {
        if (navMeshTargetDirection != null)
        {
            navMeshTargetDirection.onGetAwayFromTarget += OnGetAwayFromTarget;
            navMeshTargetDirection.onReachTarget += OnReachTarget;
            navMeshTargetDirection.onCantReachTarget += OnCantReachTarget;
            navMeshTargetDirection.onCanReachTarget += OnCanReachTarget;
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

    void OnCantReachTarget()
    {
        if (enabled)
        {
            onCantReachTarget.Invoke();
        }
    }

    void OnCanReachTarget()
    {
        if (enabled)
        {
            onCanReachTarget.Invoke();
        }
    }

    public void CheckIfCanReachTarget()
    {
        if (navMeshTargetDirection.CheckIfCanReachTarget())
        {
            if (enabled)
            {
                onCanReachTarget.Invoke();
            }
        }
    }

    private void OnDisable()
    {
        if (navMeshTargetDirection != null && listening)
        {
            navMeshTargetDirection.onGetAwayFromTarget -= OnGetAwayFromTarget;
            navMeshTargetDirection.onReachTarget -= OnReachTarget;
            navMeshTargetDirection.onCantReachTarget -= OnCantReachTarget;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
