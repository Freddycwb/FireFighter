using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterGameObjectComparison : InvokeAfter
{
    public enum ComparisonType
    {
        isEqual,
        isDifferent
    }

    [SerializeField] private ComparisonType comparison;
    [SerializeField] private GameObject objToCompare;

    public void CompareLastState(StateMachine value)
    {
        Compare(value.GetLastState());
    }

    public void Compare(GameObject value)
    {
        bool isEqual = (value == objToCompare);
        if (isEqual ^ (comparison == ComparisonType.isEqual))
        {
            CallSubAction();
        }
        else
        {
            CallAction();
        }
    }
}
