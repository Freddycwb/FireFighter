using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSetter : MonoBehaviour
{
    private GameObject _reference;

    public void SetReference(GameObject value)
    {
        _reference = value;
    }

    void Update()
    {
        if (_reference != null)
        {
            transform.eulerAngles = _reference.transform.eulerAngles;
        }        
    }
}
