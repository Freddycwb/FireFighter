using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class UISliderValue : MonoBehaviour
{
    [System.Flags]
    public enum SourceTypes
    {
        None = 0,
        counter = 1,
        timer = 2,
    }

    [SerializeField] private SourceTypes sourceType;
    [SerializeField] private GameObject source;
    [SerializeField] private GameObjectVariable sourceVariable;
    private InvokeAfterCounter _counter;
    private InvokeAfterTimer _timer;
    [SerializeField] private FloatVariable currentVariable;
    [SerializeField] private float current;
    [SerializeField] private FloatVariable maxVariable;
    [SerializeField] private float max;

    [SerializeField] private float timeToReachLowerValue;
    [SerializeField] private float timeToReachHigherValue;
    [SerializeField] private AnimationCurve curve;
    private float _currentValue;
    private float _lastValue;
    private float _count;

    [SerializeField] private Slider slider;

    private void Start()
    {
        if (sourceVariable != null)
        {
            source = sourceVariable.Value;
        }
        SetVariables();
        SetCurrentMax();
        _currentValue = current / max;
        _lastValue = _currentValue;
    }

    private void SetVariables()
    {
        if ((sourceType & SourceTypes.counter) != 0)
        {
            _counter = source.GetComponent<InvokeAfterCounter>();
        }
        else if ((sourceType & SourceTypes.counter) != 0)
        {
            _timer = source.GetComponent<InvokeAfterTimer>();
        }
    }

    public void SetCurrentMax()
    {
        if ((sourceType & SourceTypes.counter) != 0)
        {
            if (current != _counter.GetCurrentValue())
            {
                _lastValue = _currentValue;
                _count = 0;
            }
            current = _counter.GetCurrentValue();
            max = _counter.GetMaxValue();
        }
        else if ((sourceType & SourceTypes.counter) != 0)
        {
            if (current != _timer.GetCurrentTimePass())
            {
                _lastValue = _currentValue;
                _count = 0;
            }
            current = _timer.GetCurrentTimePass();
            max = _timer.GetTimeToAction();
        }
        else if (currentVariable != null)
        {
            if (current != currentVariable.Value)
            {
                _lastValue = _currentValue;
                _count = 0;
            }
            current = currentVariable.Value;
        }
        if (maxVariable != null)
        {
            max = maxVariable.Value;
        }
    }

    private void Update()
    {
        SetCurrentMax();

        if (((_currentValue > current / max) && timeToReachLowerValue > 0) || ((_currentValue < current / max) && timeToReachHigherValue > 0))
        {
            SliderLerp();
        }
        else if (((_currentValue > current / max) && timeToReachLowerValue <= 0) || ((_currentValue < current / max) && timeToReachHigherValue <= 0))
        {
            SetValueToCurrent();
        }


        slider.value = _currentValue;
    }

    private void SliderLerp()
    {
        if (_count < 1)
        {
            _count += _currentValue > current / max ? Time.deltaTime / timeToReachLowerValue : Time.deltaTime / timeToReachHigherValue;
            if (_count >= 1)
            {
                _count = 1;
            }
            _currentValue = Mathf.Lerp(_lastValue, current / max, curve.Evaluate(_count));
        }
    }

    public void SetValueToCurrent()
    {
        _currentValue = current / max;
        _lastValue = _currentValue;
        _count = 1;
    }
}
