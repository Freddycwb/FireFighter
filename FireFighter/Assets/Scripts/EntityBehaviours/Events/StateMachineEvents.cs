using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateMachineEvents : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;

    [SerializeField] private UnityEvent<GameObject> onChangeState;
    [SerializeField] private UnityEvent<bool> onCanChangeState;

    private bool listening;

    private void OnEnable()
    {
        if (stateMachine != null)
        {
            stateMachine.onChangeState += OnChangeState;
            stateMachine.onCanChangeState += OnCanChangeState;
            listening = true;
        }
    }

    void OnChangeState(GameObject value)
    {
        if (enabled)
        {
            onChangeState.Invoke(value);
        }
    }

    void OnCanChangeState(bool value)
    {
        if (enabled)
        {
            onCanChangeState.Invoke(value);
        }
    }

    private void OnDisable()
    {
        if (stateMachine != null && listening)
        {
            stateMachine.onChangeState -= OnChangeState;
            stateMachine.onCanChangeState -= OnCanChangeState;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
