using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private GameObject[] states;

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

    public void ChangeState(GameObject state)
    {
        foreach (GameObject s in states)
        {
            s.SetActive(false);
        }

        if (state != null)
        {
            state.SetActive(true);
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

    public void SetStateToNull()
    {
        ChangeState(null);
    }
}
