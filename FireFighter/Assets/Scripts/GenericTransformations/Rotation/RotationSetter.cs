using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSetter : MonoBehaviour
{
    [SerializeField] private GameObject reference;
    [SerializeField] private Vector3 offSet;

    public void SetReference(GameObject value)
    {
        reference = value;
    }

    void Update()
    {
        if (reference != null)
        {
            transform.eulerAngles = reference.transform.eulerAngles + offSet;
        }        
    }

    public void SetToZero()
    {
        transform.eulerAngles = Vector3.zero;
    }
}
