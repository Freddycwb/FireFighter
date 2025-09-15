using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        SetValueToCompare(value.Value);
    }

    public void SetValueToCompare(int value)
    {
        SetValueToCompare((float)value);
    }

    public void SetValueToCompare(IntVariable value)
    {
        SetValueToCompare(value.Value);
    }

    public void SetValueToCompare(StringArrayVariable value)
    {
        SetValueToCompare(value.Value.Length - 1);
    }

    public void SetValueToCompareByCounter(InvokeAfterCounter value)
    {
        SetValueToCompare(value.GetCurrentValue());
    }

    public void SetValueToCompareByCounter(GameObject value)
    {
        InvokeAfterCounter counter = value.GetComponent<InvokeAfterCounter>();
        if (counter != null)
        {
            SetValueToCompareByCounter(counter);
        }
    }

    public void SetValueToCompareByCounter(GameObjectVariable value)
    {
        SetValueToCompareByCounter(value.Value);
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
        Compare(value.linearVelocity.magnitude);
    }

    public void CompareInputDirection(GameObject value)
    {
        IInputDirection dir = value.GetComponent<IInputDirection>();
        if (dir != null)
        {
            Compare(dir.direction.magnitude);
        }
    }

    public void Compare(Slider value)
    {
        Compare(value.value);
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
