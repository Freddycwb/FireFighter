using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterSwitch : InvokeAfter
{
    [SerializeField] private bool value;
    private bool _value;

    public bool GetValue()
    {
        return _value;
    }

    public void SwitchValue()
    {
        value = !_value;
    }

    public void SetValue(bool newValue)
    {
        value = newValue;
    }
    public void SetValueTrue()
    {
        value = true;
    }
    public void SetValueFalse()
    {
        value = false;
    }

    private void Update()
    {
        if (value && !_value)
        {
            _value = true;
            CallAction();
        }
        else if (!value && _value)
        {
            _value = false;
            CallSubAction();
        }
    }
}
