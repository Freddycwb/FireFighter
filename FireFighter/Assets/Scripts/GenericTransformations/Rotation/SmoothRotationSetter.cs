using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotationSetter : MonoBehaviour
{
    [SerializeField] private GameObject objToRotate;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform transformTarget;
    [SerializeField] private GameObjectVariable targetVariable;

    [SerializeField] private float distToFinishRotation;
    private Vector3 vector3Target;
    private bool useVector3Target;

    private void Start()
    {
        if (targetVariable != null && targetVariable.Value != null)
        {
            transformTarget = targetVariable.Value.transform;
        }
    }

    public void SetTarget(Transform value)
    {
        transformTarget = value;
    }

    private void FixedUpdate()
    {
        if (transformTarget == null && !useVector3Target)
        {
            return;
        }

        Vector3 targetRotation = Vector3.zero;
        if (useVector3Target)
        {
            targetRotation = vector3Target + offset;
        }
        else
        {
            targetRotation = transformTarget.position + offset;
        }

        objToRotate.transform.eulerAngles = Vector3.SmoothDamp(objToRotate.transform.eulerAngles, targetRotation, ref velocity, smoothTime);

        if (useVector3Target && Vector3.Distance(objToRotate.transform.eulerAngles, targetRotation) <= distToFinishRotation)
        {
            useVector3Target = false;
            objToRotate.transform.eulerAngles = targetRotation;
        }
    }

    public void SetToZero()
    {
        useVector3Target = true;
        vector3Target = Vector3.zero;
    }
}
