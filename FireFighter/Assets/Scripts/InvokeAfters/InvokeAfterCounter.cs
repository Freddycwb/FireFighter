using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterCounter : InvokeAfter
{
    public enum valueType
    {
        Int,
        Float,
        IntVariable,
        FloatVariable,
    }

    [HideInInspector] public valueType maxValueType;
    [HideInInspector] public valueType currentValueType;
    [HideInInspector] public valueType minValueType;

    [HideInInspector] public int maxValueInt;
    [HideInInspector] public int currentValueInt;
    [HideInInspector] public int minValueInt;

    [HideInInspector] public float maxValueFloat;
    [HideInInspector] public float currentValueFloat;
    [HideInInspector] public float minValueFloat;

    [HideInInspector] public IntVariable maxValueIntVariable;
    [HideInInspector] public IntVariable currentValueIntVariable;
    [HideInInspector] public IntVariable minValueIntVariable;

    [HideInInspector] public FloatVariable maxValueFloatVariable;
    [HideInInspector] public FloatVariable currentValueFloatVariable;
    [HideInInspector] public FloatVariable minValueFloatVariable;

    private float _maxValue;
    private float _currentValue;
    private float _minValue;

    public Action onIncreaseValue;
    public Action onDecreaseValue;

    private void OnEnable()
    {
        SetMaxValue();
        SetMinValue();
        _currentValue = _maxValue;
        SetCurrentValueVariable();
    }

    public float GetMaxValue()
    {
        return _maxValue;
    }

    public float GetCurrentValue()
    {
        return _currentValue;
    }

    private void SetMaxValue()
    {
        switch (maxValueType)
        {
            case valueType.Int:
                _maxValue = Mathf.FloorToInt(maxValueInt);
                break;
            case valueType.Float:
                _maxValue = maxValueFloat;
                break;
            case valueType.IntVariable:
                _maxValue = Mathf.FloorToInt(maxValueIntVariable.Value);
                break;
            case valueType.FloatVariable:
                _maxValue = maxValueFloatVariable.Value;
                break;
            default:
                _maxValue = Mathf.FloorToInt(maxValueInt);
                break;
        }
    }

    public void SetMaxValue(int value)
    {
        _maxValue = value;
    }

    private void SetMinValue()
    {
        switch (minValueType)
        {
            case valueType.Int:
                _minValue = Mathf.FloorToInt(minValueInt);
                break;
            case valueType.Float:
                _minValue = minValueFloat;
                break;
            case valueType.IntVariable:
                _minValue = Mathf.FloorToInt(minValueIntVariable.Value);
                break;
            case valueType.FloatVariable:
                _minValue = maxValueFloatVariable.Value;
                break;
            default:
                _minValue = minValueFloatVariable.Value;
                break;
        }
    }

    private void SetCurrentValueVariable()
    {
        if (currentValueType == valueType.IntVariable)
        {
            currentValueIntVariable.Value = Mathf.FloorToInt(_currentValue);
        }
        else if (currentValueType == valueType.FloatVariable)
        {
            currentValueFloatVariable.Value = _currentValue;
        }
    }

    public void SetCurrentValueToMax()
    {
        _currentValue = _maxValue;
        CallSubAction();
    }

    public void SetCurrentValueToMin()
    {
        _currentValue = _minValue;
        CallSubAction();
    }

    public void IncreaseValue(float a)
    {
        _currentValue = Mathf.Clamp(_currentValue + a, _minValue, _maxValue);
        SetCurrentValueVariable();
        CallSubAction();
        if (onIncreaseValue != null)
        {
            onIncreaseValue.Invoke();
        }
    }

    public void IncreaseValue(FloatVariable a)
    {
        _currentValue = Mathf.Clamp(_currentValue + a.Value, _minValue, _maxValue);
        SetCurrentValueVariable();
        CallSubAction();
        if (onIncreaseValue != null)
        {
            onIncreaseValue.Invoke();
        }
    }

    public void DecreaseValue(float a)
    {
        _currentValue = Mathf.Clamp(_currentValue - a, _minValue, _maxValue);
        SetCurrentValueVariable();
        CallSubAction();
        if (_currentValue == _minValue)
        {
            CallAction();
        }
        if (onDecreaseValue != null)
        {
            onDecreaseValue.Invoke();
        }
    }

    public void DecreaseValue(FloatVariable a)
    {
        _currentValue = Mathf.Clamp(_currentValue - a.Value, _minValue, _maxValue);
        SetCurrentValueVariable();
        CallSubAction();
        if (_currentValue == _minValue)
        {
            CallAction();
        }
        if (onDecreaseValue != null)
        {
            onDecreaseValue.Invoke();
        }
    }

    public void SetValue(float a)
    {
        float lastValue = _currentValue;
        _currentValue = Mathf.Clamp(a, _minValue, _maxValue);
        SetCurrentValueVariable();
        if (lastValue != _currentValue)
        {
            CallSubAction();
            if (_currentValue == _minValue)
            {
                CallAction();
            }
        }
    }

    public void SetValue(IntVariable a)
    {
        float lastValue = _currentValue;
        _currentValue = Mathf.Clamp(a.Value, _minValue, _maxValue);
        SetCurrentValueVariable();
        if (lastValue != _currentValue)
        {
            CallSubAction();
            if (_currentValue == _minValue)
            {
                CallAction();
            }
        }
    }

    public void SetValue(FloatVariable a)
    {
        float lastValue = _currentValue;
        _currentValue = Mathf.Clamp(a.Value, _minValue, _maxValue);
        SetCurrentValueVariable();
        if (lastValue != _currentValue)
        {
            CallSubAction();
            if (_currentValue == _minValue)
            {
                CallAction();
            }
        }
    }
}
