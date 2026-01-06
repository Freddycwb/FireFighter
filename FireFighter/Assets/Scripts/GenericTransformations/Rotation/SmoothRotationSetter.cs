using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotationSetter : MonoBehaviour
{
    [SerializeField] private GameObject objToRotate;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotSpeed = 2;

    [SerializeField] private Transform transformTarget;
    [SerializeField] private GameObjectVariable targetVariable;

    private void Start()
    {
        if (objToRotate == null)
        {
            objToRotate = gameObject;
        }
        if (targetVariable != null && targetVariable.Value != null)
        {
            transformTarget = targetVariable.Value.transform;
        }
    }

    public void SetRotSpeed(float value)
    {
        rotSpeed = value;
    }

    public void SetTarget(Transform value)
    {
        transformTarget = value;
    }

    public void SetTarget(GameObjectVariable value)
    {
        transformTarget = value.Value.transform;
    }

    private void Update()
    {
        Quaternion rot = Quaternion.Slerp(objToRotate.transform.rotation, Quaternion.Euler(transformTarget.eulerAngles + offset), rotSpeed * Time.deltaTime);
        objToRotate.transform.rotation = rot;
    }
}
