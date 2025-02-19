using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterGameEventVector3 : InvokeAfter
{
    public GameEventVector3 Event;

    public UnityEvent<Vector3> vector3Action;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(Vector3 value)
    {
        CallAction();
        if (value != null)
        {
            vector3Action.Invoke(value);
        }
    }
}
