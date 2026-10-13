using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeapPositionSetterEvents : MonoBehaviour
{
    [SerializeField] private LeapPositionSetter leapPositionSetter;

    [SerializeField] private UnityEvent onStartMove;
    [SerializeField] private UnityEvent onStopMove;

    private bool listening;

    private void OnEnable()
    {
        if (leapPositionSetter != null)
        {
            leapPositionSetter.onStartLeap += OnStartMove;
            leapPositionSetter.onEndLeap += OnStopMove;
            listening = true;
        }
    }

    void OnStartMove()
    {
        if (enabled)
        {
            onStartMove.Invoke();
        }
    }

    void OnStopMove()
    {
        if (enabled)
        {
            onStopMove.Invoke();
        }
    }

    private void OnDisable()
    {
        if (leapPositionSetter != null && listening)
        {
            leapPositionSetter.onStartLeap -= OnStartMove;
            leapPositionSetter.onEndLeap -= OnStopMove;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
