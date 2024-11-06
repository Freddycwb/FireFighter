using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterDestroy : InvokeAfter
{
    [SerializeField] private List<Destroyer> destroyers = new List<Destroyer>();
    [SerializeField] private List<GameObject> objs = new List<GameObject>();

    private void OnEnable()
    {
        foreach (var destroyer in destroyers) {
            if (destroyer != null)
            {
                destroyer.onDelete += CallRefresh;
            }
        }
    }

    public void AddObject(GameObject value)
    {
        objs.Add(value);
    }

    public void AddDestroyer(GameObject value)
    {
        if (value.GetComponent<Destroyer>() != null)
        {
            destroyers.Add(value.GetComponent<Destroyer>());
            value.GetComponent<Destroyer>().onDelete += CallRefresh;
        }
    }

    private void CallRefresh(Destroyer value)
    {
        value.onDelete -= CallRefresh;
        destroyers.Remove(value);
        CallSubAction();
        CallRefresh();
    }

    public void CallRefresh()
    {
        for (int i = objs.Count - 1; i >= 0; i--) 
        {
            if (objs[i] == null)
            {
                objs.RemoveAt(i);
                CallSubAction();
            }
        }
        if (objs.Count == 0 && destroyers.Count == 0)
        {
            CallAction();
        }
    }

    private void OnDestroy()
    {
        foreach (var destroyer in destroyers)
        {
            if (destroyer != null)
            {
                destroyer.onDelete -= CallRefresh;
            }
        }
    }
}
