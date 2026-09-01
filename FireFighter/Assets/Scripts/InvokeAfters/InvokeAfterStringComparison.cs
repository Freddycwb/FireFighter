using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterStringComparison : InvokeAfter
{
    public enum ComparisonType
    {
        equal,
        contains
    }

    [SerializeField] private string compareValue;
    [SerializeField] private ComparisonType comparison;

    public void SetValueToCompare(string value)
    {
        compareValue = value;
    }

    public void SetValueToCompare(StringVariable value)
    {
        compareValue = value.Value;
    }

    public void SetValueToCompare(StringHolder value)
    {
        compareValue = value.GetString();
    }

    public void Compare(StringVariable value)
    {
        Compare(value.Value);
    }

    public void Compare(StringArrayVariable value)
    {
        bool find = false;
        switch (comparison)
        {
            case ComparisonType.equal:
                foreach (string a in value.Value)
                {
                    if (a == compareValue)
                    {
                        CallAction();
                        find = true;
                    }
                }
                if (!find)
                {
                    CallSubAction();
                }
                break;
            case ComparisonType.contains:
                foreach (string a in value.Value)
                {
                    if (a.Contains(compareValue))
                    {
                        CallAction();
                        find = true;
                    }
                }
                if (!find)
                {
                    CallSubAction();
                }
                break;
        }
    }

    public void Compare(Animator value)
    {
        Compare(value.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    }

    public void Compare(string value)
    {
        switch (comparison)
        {
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
            case ComparisonType.contains:
                if (value.Contains(compareValue))
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
