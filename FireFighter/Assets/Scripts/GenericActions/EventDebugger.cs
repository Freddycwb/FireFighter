using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDebugger : MonoBehaviour
{
    public void WriteDebug(string value)
    {
        Debug.Log(gameObject.name + " : " + value);
    }

    public void WriteErrorDebug(string value)
    {
        Debug.LogError(gameObject.name + " : " + value);
    }

    public void WriteErrorDebug(FloatVariable value)
    {
        Debug.Log(gameObject.name + " : " + value.name + " = " + value.Value);
    }

    public void WriteCounterValueDebug(InvokeAfterCounter value)
    {
        Debug.Log(gameObject.name + " : " + value.GetCurrentValue());
    }

    public void WriteTimePassDebug(InvokeAfterTimer value)
    {
        Debug.Log(gameObject.name + " : " + value.GetCurrentTimePass());
    }
}
