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

    public void CompareCanReachTarget(NavMeshTargetDirection value)
    {
        Compare(value.CheckIfCanReachTarget());
    }

    public void CompareReachTarget(NavMeshTargetDirection value)
    {
        Compare(value.GetReachTarget());
    }

    public void Compare(GameObjectHolder value)
    {
        Compare(value.GetGameObject() != null);
    }

    public void Compare(Gravity value)
    {
        Compare(value.GetIsGrounded());
    }

    public void Compare(InvokeAfterSwitch value)
    {
        Compare(value.GetValue());
    }

    public void Compare(GameObject value)
    {
        Compare(value.activeSelf);
    }

    public void Compare(BoolVariable value)
    {
        Compare(value.Value);
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
