using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaneMoveEvents : MonoBehaviour
{
    [SerializeField] private PlaneMove planeMove;

    [SerializeField] private UnityEvent onStartMove;
    [SerializeField] private UnityEvent onStopMove;

    private bool listening;

    private void Start()
    {
        if (planeMove != null)
        {
            planeMove.onStartMove += OnStartMove;
            planeMove.onStopMove += OnStopMove;
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
        if (planeMove != null && listening)
        {
            planeMove.onStartMove -= OnStartMove;
            planeMove.onStopMove -= OnStopMove;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
