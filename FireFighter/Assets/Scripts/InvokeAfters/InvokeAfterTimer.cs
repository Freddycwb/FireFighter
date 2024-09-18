using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterTimer : InvokeAfter
{
    [SerializeField] private float timeToAction;
    [SerializeField] private InvokeAfterTimerValueSetter valueSetter;
    private float maxTimeToAction;
    private FloatVariable timeToActionVariable;
    private Vector2Variable randomTimeToActionVariable;
    private float valueAdjuster;
    [SerializeField] private bool startTimerOnEnabled;
    [SerializeField] private bool overrideLastTimer = true;
    [SerializeField] private bool useUnscaledTime;

    private float _currentTimeToAction;
    private bool _isPaused;
    private float _currentTimePass;

    private Coroutine coroutine;

    public float GetTimeToAction()
    {
        return timeToAction;
    }

    public float GetCurrentTimeToAction()
    {
        return _currentTimeToAction;
    }

    public bool GetIsPaused()
    {
        return _isPaused;
    }

    public float GetCurrentTimePass()
    {
        return _currentTimePass;
    }

    public void SetPause(bool value)
    {
        _isPaused = value;
    }

    private void OnEnable()
    {
        if (valueSetter != null)
        {
            maxTimeToAction = valueSetter.GetMaxTimeToAction();
            timeToActionVariable = valueSetter.GetTimeToActionVariable();
            randomTimeToActionVariable = valueSetter.GetRandomTimeToActionVariable();
            valueAdjuster = valueSetter.GetValueAdjuster();
        }
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
        if (timeToAction > 0 || (randomTimeToActionVariable != null && randomTimeToActionVariable.Value != Vector2.zero) || (timeToActionVariable != null && timeToActionVariable.Value > 0))
        {
            coroutine = StartCoroutine(InvokeAfterSeconds());
        }
        CallSubAction();
    }

    private IEnumerator InvokeAfterSeconds()
    {
        if (randomTimeToActionVariable != null && randomTimeToActionVariable.Value != Vector2.zero)
        {
            _currentTimeToAction = Random.Range(randomTimeToActionVariable.Value.x, randomTimeToActionVariable.Value.y);
            yield return Timer(_currentTimeToAction);
        }
        else if (timeToActionVariable != null && timeToActionVariable.Value > 0)
        {
            _currentTimeToAction = timeToActionVariable.Value;
            yield return Timer(_currentTimeToAction);
        }
        else
        {
            if (maxTimeToAction <= 0)
            {
                _currentTimeToAction = timeToAction;
                yield return Timer(_currentTimeToAction);
            }
            else
            {
                _currentTimeToAction = Random.Range(timeToAction, maxTimeToAction);
                yield return Timer(_currentTimeToAction);
            }
        }
        CallAction();
    }

    private IEnumerator Timer(float timeCount)
    {
        timeCount += valueAdjuster;
        while (timeCount > 0)
        {
            _currentTimePass = timeCount;
            if (!_isPaused)
            {
                if (useUnscaledTime)
                {
                    timeCount -= Time.unscaledDeltaTime;
                }
                else
                {
                    timeCount -= Time.deltaTime;
                }
            }
            yield return null;
        }
    }

    public void SetTimeToAction(float time)
    {
        timeToAction = time;
        StartCoroutine(InvokeAfterSeconds());
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
