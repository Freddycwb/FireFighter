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

    private OperatorType.Type valueAdjustType;

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
            valueAdjustType = valueSetter.GetValueAdjustType();
        }
        if (startTimerOnEnabled)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        if (overrideLastTimer && coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        enabled = true;
        bool haveTime = timeToAction > 0 || (randomTimeToActionVariable != null && randomTimeToActionVariable.Value != Vector2.zero) || (timeToActionVariable != null && timeToActionVariable.Value > 0);
        if (haveTime)
        {
            coroutine = StartCoroutine(InvokeAfterSeconds());
        }
        if (!haveTime)
        {
            CallSubAction();
            CallAction();
        }
    }

    private IEnumerator InvokeAfterSeconds()
    {
        if (randomTimeToActionVariable != null && randomTimeToActionVariable.Value != Vector2.zero)
        {
            _currentTimeToAction = Random.Range(randomTimeToActionVariable.Value.x, randomTimeToActionVariable.Value.y);
        }
        else if (timeToActionVariable != null && timeToActionVariable.Value > 0)
        {
            _currentTimeToAction = timeToActionVariable.Value;
        }
        else
        {
            if (maxTimeToAction <= 0)
            {
                _currentTimeToAction = timeToAction;
            }
            else
            {
                _currentTimeToAction = Random.Range(timeToAction, maxTimeToAction);
            }
        }
        CallSubAction();
        yield return Timer(_currentTimeToAction);
        CallAction();
    }

    private IEnumerator Timer(float timeCount)
    {
        switch (valueAdjustType)
        {
            case OperatorType.Type.add:
                timeCount += valueAdjuster;
                break;
            case OperatorType.Type.subtract:
                timeCount -= valueAdjuster;
                break;
            case OperatorType.Type.divide:
                timeCount /= valueAdjuster;
                break;
            case OperatorType.Type.multiply:
                timeCount *= valueAdjuster;
                break;
            default: 
                break;
        }
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
    }

    public void SetTimeToAction(DamageChecker time)
    {
        timeToAction = time.GetLastDamage();
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
