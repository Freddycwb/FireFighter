using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterGameEventString : InvokeAfter
{
    public UnityEvent<string> stringAction;
    public GameEventString Event;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(string value)
    {
        CallAction();
        if (stringAction != null)
        {
            stringAction.Invoke(value);
        }
    }
}
