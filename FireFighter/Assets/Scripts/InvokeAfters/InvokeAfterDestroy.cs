using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterDestroy : InvokeAfter
{
    [SerializeField] private List<Destroyer> destroyers = new List<Destroyer>();

    private void OnEnable()
    {
        foreach (var destroyer in destroyers) {
            if (destroyer != null)
            {
                destroyer.onDelete += CallRefresh;
            }
        }
    }

    public void AddGameObject(GameObject value)
    {
        if (value.GetComponent<Destroyer>() != null)
        {
            destroyers.Add(value.GetComponent<Destroyer>());
            value.GetComponent<Destroyer>().onDelete += CallRefresh;
        }
    }

    private void CallRefresh()
    {
        //StartCoroutine(RefreshList());
    }

    private IEnumerator RefreshList()
    {
        foreach (var destroyer in destroyers)
        {
            if (destroyer != null)
            {
                destroyer.onDelete -= CallRefresh;
            }
        }
        yield return new WaitForSeconds(0.05f);
        CallSubAction();
        destroyers = destroyers.Where(x => x != null).ToList();
        if (destroyers.Count == 0)
        {
            CallAction();
        }
        foreach (var destroyer in destroyers)
        {
            if (destroyer != null)
            {
                destroyer.onDelete += CallRefresh;
            }
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
