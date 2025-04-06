using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSetter : MonoBehaviour
{
    [SerializeField] private GameObject objectToRotate;
    [SerializeField] private GameObject reference;
    [SerializeField] private Vector3 offSet;

    private void Awake()
    {
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
}
