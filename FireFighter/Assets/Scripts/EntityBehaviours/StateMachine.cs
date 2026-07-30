using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private GameObject[] states;

    [ReadOnly][SerializeField] private GameObject currentState;
    [ReadOnly][SerializeField] private GameObject lastState;
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

    public GameObject GetLastState()
    {
        return lastState;
    }

    public void ChangeState(GameObject state)
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

    public void SetStateToNull()
    {
        ChangeState(null);
    }
}
