using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoosterEvents : MonoBehaviour
{
    [SerializeField] private Booster booster;

    [SerializeField] private UnityEvent onActive;
    [SerializeField] private UnityEvent onInactive;

    private bool listening;

    private void OnEnable()
    {
        if (booster != null)
        {
            booster.onActive += OnActive;
            booster.onInactive += OnInactive;
            listening = true;
        }
    }

    void OnActive()
    {
        if (enabled)
        {
            onActive.Invoke();
        }
    }

    void OnInactive()
    {
        if (enabled)
        {
            onInactive.Invoke();
        }
    }

    private void OnDisable()
    {
        if (booster != null && listening)
        {
            booster.onActive -= OnActive;
            booster.onInactive -= OnInactive;
            listening = true;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
