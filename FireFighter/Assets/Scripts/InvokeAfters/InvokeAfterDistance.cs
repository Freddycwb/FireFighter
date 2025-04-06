using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterDistance : InvokeAfter
{
    [SerializeField] private GameObject origin;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObjectVariable targetVariable;
    [SerializeField] private float distanceToAction;
    [SerializeField] private bool doActionOnStart = true;

    private Transform originTransform;
    private Transform targetTransform;
    private bool _close;

    private void Start()
    {
        if (origin == null)
        {
            origin = gameObject;
        }
        if (targetVariable != null)
        {
            target = targetVariable.Value;
        }
        SetOrigin(origin);
        SetTarget(target);
        if (doActionOnStart)
        {
            if (Vector3.Distance(originTransform.position, targetTransform.position) <= distanceToAction)
            {
                _close = true;
                CallAction();
            }
            else if (Vector3.Distance(originTransform.position, targetTransform.position) > distanceToAction)
            {
                _close = false;
                CallSubAction();
            }
        }
    }

    public bool GetClose()
    {
        return _close;
    }

    public void SetOrigin(GameObject value)
    {
        originTransform = value.transform;
    }

    public void SetTarget(GameObject value)
    {
        targetTransform = value.transform;
    }

    private void Update()
    {
        if (Vector3.Distance(originTransform.position, targetTransform.position) <= distanceToAction && !_close)
        {
            _close = true;
            CallAction();
        }
        else if (Vector3.Distance(originTransform.position, targetTransform.position) > distanceToAction && _close)
        {
            _close = false;
            CallSubAction();
        }
    }
}
