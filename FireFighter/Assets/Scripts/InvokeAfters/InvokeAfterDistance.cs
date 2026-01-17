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
    [SerializeField] private bool doActionOnEnable = true;

    private Transform originTransform;
    private Transform targetTransform;
    private bool _close;

    private void OnEnable()
    {
        InitialCheck();
    }

    private void Start()
    {
        InitialCheck();
    }

    private void InitialCheck()
    {
        if (origin == null)
        {
            origin = gameObject;
        }
        if (targetVariable != null)
        {
            target = targetVariable.Value;
        }

        if (origin != null)
        {
            SetOrigin(origin);
        }
        if (target != null)
        {
            SetTarget(target);
        }

        if (doActionOnEnable && origin != null && target != null)
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

    public void SetDistance(float value)
    {
        distanceToAction = value;
    }

    private void Update()
    {
        if (originTransform == null || targetTransform == null)
        {
            return;
        }
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
