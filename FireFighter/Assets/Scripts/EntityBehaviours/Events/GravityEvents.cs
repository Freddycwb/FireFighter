using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityEvents : MonoBehaviour
{
    [SerializeField] private Gravity gravity;

    [SerializeField] private UnityEvent onLand;
    [SerializeField] private UnityEvent onTakeOff;

    private bool listening;

    private void OnEnable()
    {
        if (gravity != null)
        {
            gravity.onLand += OnLand;
            gravity.onTakeOff += OnTakeOff;
            listening = true;
        }
    }

    void OnLand()
    {
        onLand.Invoke();
    }

    void OnTakeOff()
    {
        onTakeOff.Invoke();
    }

    private void OnDisable()
    {
        if (gravity != null && listening)
        {
            gravity.onLand -= OnLand;
            gravity.onTakeOff -= OnTakeOff;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
