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
        currentTimeToAction = timer.GetCurrentTimeToAction();
        isPaused = timer.GetIsPaused();
        currentTimePass = timer.GetCurrentTimePass();
    }
}
