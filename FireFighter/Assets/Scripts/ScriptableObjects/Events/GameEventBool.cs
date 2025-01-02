using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventBool : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<InvokeAfterGameEventBool> _eventListeners =
        new List<InvokeAfterGameEventBool>();

    public bool value;

    public void Raise()
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
            _eventListeners[i].OnEventRaised(value);
    }

    public void Raise(bool value)
    {
        this.value = value;
        Raise();
    }

    public void RegisterListener(InvokeAfterGameEventBool listener)
    {
        if (!_eventListeners.Contains(listener))
            _eventListeners.Add(listener);
    }

    public void UnregisterListener(InvokeAfterGameEventBool listener)
    {
        if (_eventListeners.Contains(listener))
            _eventListeners.Remove(listener);
    }
}