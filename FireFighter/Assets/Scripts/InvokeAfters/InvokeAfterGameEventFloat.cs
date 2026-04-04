using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterGameEventFloat : InvokeAfter
{
    public UnityEvent<float> floatAction;
    public GameEventFloat Event;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(float value)
    {
        CallAction();
        if (floatAction != null)
        {
            floatAction.Invoke(value);
        }
    }
}
