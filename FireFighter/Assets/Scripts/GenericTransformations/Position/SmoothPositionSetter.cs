using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothPositionSetter : MonoBehaviour
{
    [SerializeField] private GameObject objToMove;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private GameObjectVariable targetVariable;

    private void Start()
    {
        if (objToMove == null)
        {
            objToMove = gameObject;
        }
        if (targetVariable != null && targetVariable.Value != null)
        {
            target = targetVariable.Value.transform;
        }
    }

    public void SetSmoothTime(float value)
    {
        smoothTime = value;
    }

    public void SetTarget(Transform value)
    {
        target = value;
    }

    public void SetTarget(GameObjectVariable value)
    {
        target = value.Value.transform;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        Vector3 targetPosition = target.position + offset;
        objToMove.transform.position = Vector3.SmoothDamp(objToMove.transform.position, targetPosition, ref velocity, smoothTime);
    }
}
