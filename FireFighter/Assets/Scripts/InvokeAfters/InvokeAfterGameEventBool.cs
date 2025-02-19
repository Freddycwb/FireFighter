using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InvokeAfterGameEventBool : InvokeAfter
{
    public GameEventBool Event;

    public UnityEvent<bool> boolAction;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(bool value)
    {
        if (value)
        {
            CallAction();
        }
        else
        {
            CallSubAction();
        }
    }
}
