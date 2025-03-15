using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventGameObject : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<InvokeAfterGameEventGameObject> _eventListeners =
        new List<InvokeAfterGameEventGameObject>();

    public GameObject value;

    public void Raise()
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
            _eventListeners[i].OnEventRaised(value);
    }

    public void Raise(GameObject value)
    {
        this.value = value;
        Raise();
    }

    public void RegisterListener(InvokeAfterGameEventGameObject listener)
    {
        if (!_eventListeners.Contains(listener))
            _eventListeners.Add(listener);
    }

    public void UnregisterListener(InvokeAfterGameEventGameObject listener)
    {
        if (_eventListeners.Contains(listener))
            _eventListeners.Remove(listener);
    }
}