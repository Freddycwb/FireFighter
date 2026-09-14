using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private GameObject[] states;

    [SerializeField] private bool canChangeState = true;
    public Action<bool> onCanChangeState;

    [ReadOnly][SerializeField] private GameObject currentState;
    [ReadOnly][SerializeField] private GameObject lastState;
    [ReadOnly][SerializeField] private GameObject lastStateRequired;
    public Action<GameObject> onChangeState;

    private void Start()
    {
        foreach (GameObject s in states)
        {
            if (s.activeSelf)
            {
                ChangeState(s);
                break;
            }
        }
    }

    public GameObject GetCurrentState()
    {
        return currentState;
    }

    public GameObject GetLastState()
    {
        return lastState;
    }

    public void SetLastStateRequeredToNull()
    {
        lastStateRequired = null;
    }

    public void SetCanChangeState(bool value)
    {
        if (canChangeState == value)
        {
            return;
        }
        canChangeState = value;
        if (value && lastStateRequired != null)
        {
            ChangeState(lastStateRequired);
            lastState = null;
        }
        if (onCanChangeState != null)
        {
            onCanChangeState.Invoke(canChangeState);
        }
    }

    public void ChangeState(GameObject state)
    {
        if (state == null)
        {
            return;
        }
        if (!canChangeState)
        {
            lastStateRequired = state;
            return;
        }
        ChangeStateAction(state);
    }

    public void ForcedChangeState(GameObject state)
    {
        ChangeStateAction(state);
    }

    private void ChangeStateAction(GameObject state)
    {
        foreach (GameObject s in states)
        {
            s.SetActive(false);
        }

        if (state != null)
        {
            if (currentState != state)
            {
                if (currentState != lastState)
                {
                    lastState = currentState;
                }
            }
            currentState = state;
            currentState.SetActive(true);
            if (onChangeState != null)
            {
                onChangeState.Invoke(state);
            }
        }
        else
        {
            if (onChangeState != null)
            {
                onChangeState.Invoke(gameObject);
            }
        }
    }

    public void SetStateToLastState()
    {
        ChangeState(lastState);
    }
}
