using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotateToDirectionOneAxisEvents : MonoBehaviour
{
    [SerializeField] private RotateToDirectionOneAxis rotateToDirectionOneAxis;

    [SerializeField] private UnityEvent onStartMove;
    [SerializeField] private UnityEvent onStopMove;

    private bool listening;

    private void OnEnable()
    {
        if (rotateToDirectionOneAxis != null)
        {
            rotateToDirectionOneAxis.onStartMove += OnStartMove;
            rotateToDirectionOneAxis.onStopMove += OnStopMove;
            listening = true;
        }
    }

    void OnStartMove()
    {
        onStartMove.Invoke();
    }

    void OnStopMove()
    {
        onStopMove.Invoke();
    }

    private void OnDisable()
    {
        if (rotateToDirectionOneAxis != null && listening)
        {
            rotateToDirectionOneAxis.onStartMove -= OnStartMove;
            rotateToDirectionOneAxis.onStopMove -= OnStopMove;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
