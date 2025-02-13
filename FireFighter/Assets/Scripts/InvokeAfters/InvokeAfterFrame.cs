using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterFrame : InvokeAfter
{
    [SerializeField] private int framesToAction;

    [SerializeField] private bool startTimerOnEnabled;
    [SerializeField] private bool overrideLastTimer = true;
    [SerializeField] private bool useUnscaledTime;

    private float _currentTimeToAction;
    private float _currentTimePass;

    private Coroutine coroutine;

    private void OnEnable()
    {
        if (startTimerOnEnabled)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        if (overrideLastTimer && coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        enabled = true;
        if (framesToAction > 0)
        {
            coroutine = StartCoroutine(InvokeAfterFrames());
        }
        CallSubAction();
        if (framesToAction <= 0)
        {
            CallAction();
        }
    }

    private IEnumerator InvokeAfterFrames()
    {
        int i = 0;
        while (i < framesToAction)
        {
            yield return new WaitForNextFrameUnit();
            if (Time.timeScale > 0 || (Time.timeScale <= 0 && useUnscaledTime))
            {
                i++;
            }
        }
        CallAction();
    }

    public void SetTimeToAction(int time)
    {
        framesToAction = time;
        StartTimer();
    }

    public void CancelTimer()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
