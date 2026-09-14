using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterTimerEvents : MonoBehaviour
{
    [SerializeField] private InvokeAfterTimer invokeAfterTimer;

    [SerializeField] private UnityEvent onTimerCanceled;

    private bool listening;

    private void OnEnable()
    {
        if (invokeAfterTimer != null && !listening)
        {
            invokeAfterTimer.onTimerCanceled += OnTimerCanceled;
            listening = true;
        }
    }

    public void SetInvokeAfterTimer(InvokeAfterTimer value)
    {
        invokeAfterTimer = value;
        if (invokeAfterTimer != null && !listening)
        {
            invokeAfterTimer.onTimerCanceled += OnTimerCanceled;
            listening = true;
        }
    }

    void OnTimerCanceled()
    {
        if (enabled)
        {
            onTimerCanceled.Invoke();
        }
    }

    private void OnDisable()
    {
        if (invokeAfterTimer != null && !listening)
        {
            invokeAfterTimer.onTimerCanceled -= OnTimerCanceled;
            listening = true;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
