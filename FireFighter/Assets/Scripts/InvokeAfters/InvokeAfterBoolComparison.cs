using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterBoolComparison : InvokeAfter
{
    public enum ComparisonType
    {
        isTrue,
        isFalse
    }

    [SerializeField] private ComparisonType comparison;

    public void Compare(Gravity value)
    {
        Compare(value.GetIsGrounded());
    }

    public void Compare(GameObject value)
    {
        Compare(value.activeSelf);
    }

    public void Compare(bool value)
    {
        if (value ^ (comparison == ComparisonType.isTrue))
        {
            CallSubAction();
        }
        else
        {
            CallAction();
        }
    }
}
