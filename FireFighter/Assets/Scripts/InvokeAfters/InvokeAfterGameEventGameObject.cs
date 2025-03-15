using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterGameEventGameObject : InvokeAfter
{
    public GameEventGameObject Event;

    public UnityEvent<GameObject> gameObjectAction;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public virtual void OnEventRaised(GameObject value)
    {
        CallAction();
        if (value != null)
        {
            gameObjectAction.Invoke(value);
        }
    }
}
