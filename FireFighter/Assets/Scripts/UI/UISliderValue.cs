using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private bool autoUpdate = true;
    private InvokeAfterCounter _counter;
    private InvokeAfterTimer _timer;
    [SerializeField] private FloatVariable minVariable;
    [SerializeField] private float min;
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
        SetMinCurrentMax();
        _currentValue = (current - min) / (max - min);
        _lastValue = _currentValue;
        if (!autoUpdate)
        {
            UpdateSliderValue();
        }
    }

    private void SetVariables()
    {
        if (source == null)
        {
            return;
        }
        if ((sourceType & SourceTypes.counter) != 0)
        {
            _counter = source.GetComponent<InvokeAfterCounter>() != null ? source.GetComponent<InvokeAfterCounter>() : null;
        }
        else if ((sourceType & SourceTypes.counter) != 0)
        {
            _timer = source.GetComponent<InvokeAfterTimer>() != null ? source.GetComponent<InvokeAfterTimer>() : null;
        }
    }

    public void SetMinCurrentMax()
    {
        if (((sourceType & SourceTypes.counter) != 0) && _counter != null)
        {
            if (current != _counter.GetCurrentValue())
            {
                _lastValue = _currentValue;
                _count = 0;
            }
            min = _counter.GetMinValue();
            current = _counter.GetCurrentValue();
            max = _counter.GetMaxValue();
        }
        else if (((sourceType & SourceTypes.timer) != 0) && _timer != null)
        {
            if (current != _timer.GetCurrentTimePass())
            {
                _lastValue = _currentValue;
                _count = 0;
            }
            min = 0;
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
        if (minVariable != null)
        {
            min = minVariable.Value;
        }
        if (maxVariable != null)
        {
            max = maxVariable.Value;
        }
    }

    private void Update()
    {
        if (!autoUpdate)
        {
            return;
        }
        SetMinCurrentMax();

        float newCurrent = (current - min) / (max - min);
        if (((_currentValue > newCurrent) && timeToReachLowerValue > 0) || ((_currentValue < newCurrent) && timeToReachHigherValue > 0))
        {
            SliderLerp();
        }
        else if (((_currentValue > newCurrent) && timeToReachLowerValue <= 0) || ((_currentValue < newCurrent) && timeToReachHigherValue <= 0))
        {
            SetValueToCurrent();
        }

        slider.value = _currentValue;
    }

    public void UpdateSliderValue()
    {
        SetMinCurrentMax();
        SetValueToCurrent();
        slider.value = _currentValue;
    }

    private void SliderLerp()
    {
        if (_count < 1)
        {
            float newCurrent = (current - min) / (max - min);
            _count += _currentValue > newCurrent ? Time.deltaTime / timeToReachLowerValue : Time.deltaTime / timeToReachHigherValue;
            if (_count >= 1)
            {
                _count = 1;
            }
            _currentValue = Mathf.Lerp(_lastValue, newCurrent, curve.Evaluate(_count));
        }
    }

    public void SetValueToCurrent()
    {
        float newCurrent = (current - min) / (max - min);
        _currentValue = newCurrent;
        _lastValue = _currentValue;
        _count = 1;
    }

    public void SetCounterCurrentValue()
    {
        if ((sourceType & SourceTypes.counter) != 0)
        {
            _counter.SetValue((max - min) * slider.value + min);
        }
    }
}
