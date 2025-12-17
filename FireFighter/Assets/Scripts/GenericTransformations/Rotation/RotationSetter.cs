using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSetter : MonoBehaviour
{
    [SerializeField] private GameObject objectToRotate;
    [SerializeField] private GameObject reference;
    [SerializeField] private Vector3 offSet;

    private Vector3 initialEulerAngles;

    private void Awake()
    {
        initialEulerAngles = transform.eulerAngles;
        if (objectToRotate == null) 
        {
            objectToRotate = gameObject;
        }
    }

    public void SetReference(GameObject value)
    {
        reference = value;
    }

    void Update()
    {
        if (reference != null)
        {
            SetRotationToReference(reference);
        }        
    }

    public void SetRotationToReference(GameObject value)
    {
        objectToRotate.transform.eulerAngles = value.transform.eulerAngles + offSet;
    }

    public void SetLocalRotationToReference(GameObject value)
    {
        objectToRotate.transform.localEulerAngles = value.transform.localEulerAngles + offSet;
    }

    public void SetToZero()
    {
        objectToRotate.transform.eulerAngles = Vector3.zero;
    }

    public void SetLocalToZero()
    {
        objectToRotate.transform.localEulerAngles = Vector3.zero;
    }

    public void SetLocalX(float value)
    {
        objectToRotate.transform.localEulerAngles = new Vector3(value, objectToRotate.transform.localEulerAngles.y, objectToRotate.transform.localEulerAngles.z);
    }

    public void SetLocalY(float value)
    {
        objectToRotate.transform.localEulerAngles = new Vector3(objectToRotate.transform.localEulerAngles.x, value, objectToRotate.transform.localEulerAngles.z);
    }

    public void SetLocalZ(float value)
    {
        objectToRotate.transform.localEulerAngles = new Vector3(objectToRotate.transform.localEulerAngles.x, objectToRotate.transform.localEulerAngles.y, value);
    }

    public void SetToInitial()
    {
        objectToRotate.transform.eulerAngles = initialEulerAngles;
    }
}
