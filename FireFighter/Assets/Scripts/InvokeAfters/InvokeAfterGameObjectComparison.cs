using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterGameObjectComparison : InvokeAfter
{
    [SerializeField] private UnityEvent<GameObject> actionGameObject;
    [SerializeField] private UnityEvent<GameObject> subActionGameObject;

    public enum ComparisonType
    {
        isEqual,
        isDifferent
    }

    [SerializeField] private ComparisonType comparison;
    [SerializeField] private GameObject objToCompare;

    public void CompareCurrentState(StateMachine value)
    {
        Compare(value.GetCurrentState());
    }

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
            subActionGameObject.Invoke(value);
        }
        else
        {
            CallAction();
            actionGameObject.Invoke(value);
        }
    }
}
