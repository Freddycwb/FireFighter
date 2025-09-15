using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpEvents : MonoBehaviour
{
    [SerializeField] private Jump jump;

    [SerializeField] private UnityEvent onJump;
    [SerializeField] private UnityEvent onStopHolding;
    [SerializeField] private bool callBothEventsOnMaxHeight = true;
    [SerializeField] private UnityEvent onStopGainingHeight;

    private bool listening;

    private void OnEnable()
    {
        if (jump != null)
        {
            jump.onJump += OnJump;
            jump.onStopHolding += OnStopHolding;
            jump.onStopGainingHeight += OnStopGainingHeight;
            listening = true;
        }
    }

    void OnJump()
    {
        if (enabled)
        {
            onJump.Invoke();
        }
    }

    void OnStopHolding()
    {
        if (enabled)
        {
            onStopHolding.Invoke();
        }
    }

    void OnStopGainingHeight()
    {
        if (enabled)
        {
            onStopGainingHeight.Invoke();
            if (callBothEventsOnMaxHeight)
            {
                OnStopHolding();
            }
        }
    }

    private void OnDisable()
    {
        if (jump != null && listening)
        {
            jump.onJump -= OnJump;
            jump.onStopHolding -= OnStopHolding;
            jump.onStopGainingHeight -= OnStopGainingHeight;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
