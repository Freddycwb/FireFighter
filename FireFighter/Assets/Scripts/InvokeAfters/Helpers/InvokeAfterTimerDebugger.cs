using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterTimerDebugger : MonoBehaviour
{
    [SerializeField] private InvokeAfterTimer timer;

    [SerializeField] private float currentTimeToAction;
    [SerializeField] private bool isPaused;
    [SerializeField] private float currentTimePass;

    private Coroutine coroutine;

    private void Update()
    {
        timer.GetCurrentTimeToAction();
        timer.GetIsPaused();
        timer.GetcurrentTimePass();
    }
}
