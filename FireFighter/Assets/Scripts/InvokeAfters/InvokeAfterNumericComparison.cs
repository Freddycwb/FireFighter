using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterNumericComparison : InvokeAfter
{
    public enum ComparisonType
    {
        less,
        lessOrEqual,
        equal,
        equalOrGreater,
        greater
    }

    [SerializeField] private float compareValue;
    [SerializeField] private ComparisonType comparison;

    public void SetValueToCompare(float value)
    {
        compareValue = value;
    }

    public void SetValueToCompare(FloatVariable value)
    {
        compareValue = value.Value;
    }

    public void SetValueToCompare(int value)
    {
        compareValue = value;
    }

    public void SetValueToCompare(IntVariable value)
    {
        compareValue = value.Value;
    }

    public void SetValueToCompare(StringArrayVariable value)
    {
        compareValue = value.Value.Length - 1;
    }

    public void Compare(int value)
    {
        Compare((float)value);
    }

    public void Compare(FloatVariable value)
    {
        Compare(value.Value);
    }

    public void Compare(IntVariable value)
    {
        Compare(value.Value);
    }

    public void Compare(InvokeAfterCollision value)
    {
        Compare(value.GetNumberOfCollisions());
    }

    public void Compare(InvokeAfterCounter value)
    {
        Compare(value.GetCurrentValue());
    }

    public void Compare(Rigidbody value)
    {
        Compare(value.velocity.magnitude);
    }

    public void Compare(float value)
    {
        switch (comparison)
        {
            case ComparisonType.less:
                if (value < compareValue)
                {
                    CallAction();
                }
                else
                {
                    CallSubAction();
                }
                break;
            case ComparisonType.lessOrEqual:
                if (value <= compareValue)
                {
                    CallAction();
                }
                else
                {
                    CallSubAction();
                }
                break;
            case ComparisonType.equal:
                if (value == compareValue)
                {
                    CallAction();
                }
                else
                {
                    CallSubAction();
                }
                break;
            case ComparisonType.equalOrGreater:
                if (value >= compareValue)
                {
                    CallAction();
                }
                else
                {
                    CallSubAction();
                }
                break;
            case ComparisonType.greater:
                if (value > compareValue)
                {
                    CallAction();
                }
                else
                {
                    CallSubAction();
                }
                break;
        }
    }
}
