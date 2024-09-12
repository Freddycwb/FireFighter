using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothPositionSetter : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private GameObjectVariable targetVariable;

    private void OnEnable()
    {
        if (targetVariable != null)
        {
            target = targetVariable.Value.transform;
        }
    }

    public void SetTarget(Transform value)
    {
        target = value;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
