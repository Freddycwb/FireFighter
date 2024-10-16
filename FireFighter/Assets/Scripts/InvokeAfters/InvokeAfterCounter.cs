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

    public enum startValueType
    {
        Min,
        Current,
        Max
    }

    public enum actionValueType
    {
        Min,
        Max
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

    [SerializeField] private actionValueType targetToAction = actionValueType.Min;
    [SerializeField] private startValueType startCurrentValueType = startValueType.Max;

    public Action onModifyValue;

    private void OnEnable()
    {
        SetMaxValue();
        SetMinValue();
        SetStartCurrentValue();
        SetCurrentValueVariable();
    }

    private void SetStartCurrentValue()
    {
        switch (startCurrentValueType)
        {
            case startValueType.Min:
                _currentValue = _minValue;
                break;
            case startValueType.Current:
                break;
            case startValueType.Max:
                _currentValue = _maxValue;
                break;
            default:
                break;
        }
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

    private void CheckAction()
    {
        switch (targetToAction)
        {
            case actionValueType.Min:
                if (_currentValue == _minValue)
                {
                    CallAction();
                }
                break;
            case actionValueType.Max:
                if (_currentValue == _maxValue)
                {
                    CallAction();
                }
                break;
        }
    }

    public void ModifyValue(float a)
    {
        _currentValue = Mathf.Clamp(_currentValue + a, _minValue, _maxValue);
        SetCurrentValueVariable();
        CallSubAction();
        CheckAction();
        if (onModifyValue != null)
        {
            onModifyValue.Invoke();
        }
    }

    public void ModifyValue(FloatVariable a)
    {
        _currentValue = Mathf.Clamp(_currentValue + a.Value, _minValue, _maxValue);
        SetCurrentValueVariable();
        CallSubAction();
        CheckAction();
        if (onModifyValue != null)
        {
            onModifyValue.Invoke();
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
            CheckAction();
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
            CheckAction();
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
            CheckAction();
        }
    }
}
