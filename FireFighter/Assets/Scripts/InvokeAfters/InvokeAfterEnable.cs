using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterEnable : InvokeAfter
{
    private void OnEnable()
    {
        CallAction();
    }

    private void OnDisable()
    {
        CallSubAction();
    }
}
