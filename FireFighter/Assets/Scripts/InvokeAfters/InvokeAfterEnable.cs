using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterEnable : InvokeAfter
{
    [SerializeField] private bool activeInHierarchy;

    private void OnEnable()
    {
        if (gameObject.activeInHierarchy || !activeInHierarchy)
        {
            CallAction();
        }
    }

    private void OnDisable()
    {
        CallSubAction();
    }
}
