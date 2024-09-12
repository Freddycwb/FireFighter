using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterTimerValueSetter : MonoBehaviour
{
    [SerializeField] private float maxTimeToAction;
    [SerializeField] private FloatVariable timeToActionVariable;
    [SerializeField] private Vector2Variable randomTimeToActionVariable;
    [SerializeField] private float valueAdjuster;
    [SerializeField] private FloatVariable valueAdjusterVariable;

    private Coroutine coroutine;

    public float GetMaxTimeToAction()
    {
        return maxTimeToAction;
    }

    public FloatVariable GetTimeToActionVariable()
    {
        return timeToActionVariable;
    }

    public Vector2Variable GetRandomTimeToActionVariable()
    {
        return randomTimeToActionVariable;
    }

    public float GetValueAdjuster()
    {
        if (valueAdjusterVariable != null)
        {
            valueAdjuster = valueAdjusterVariable.Value;
        }
        return valueAdjuster;
    }
}
