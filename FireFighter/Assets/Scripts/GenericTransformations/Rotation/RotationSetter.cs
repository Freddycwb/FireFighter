using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSetter : MonoBehaviour
{
    [SerializeField] private GameObject objectToRotate;
    [SerializeField] private GameObject reference;
    [SerializeField] private Vector3 offSet;

    private Vector3 initialEulerAngles;
    private bool initialSetted = false;

    private void Awake()
    {
        SetInitial();
        if (objectToRotate == null) 
        {
            objectToRotate = gameObject;
        }
    }

    private void SetInitial()
    {
        if (!initialSetted)
        {
            initialEulerAngles = objectToRotate.transform.eulerAngles;
            initialSetted = true;
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

    public void SetRotation(GameObject value)
    {
        SetInitial();
        objectToRotate.transform.eulerAngles = value.transform.eulerAngles;
    }

    public void SetRotation(GameObjectVariable value)
    {
        SetRotation(value.Value);
    }

    public void SetRotation(GameObjectHolder value)
    {
        SetRotation(value.GetGameObject());
    }

    public void SetRotationToReference(GameObject value)
    {
        SetInitial();
        objectToRotate.transform.eulerAngles = value.transform.eulerAngles + offSet;
    }

    public void SetLocalRotationToReference(GameObject value)
    {
        SetInitial();
        objectToRotate.transform.localEulerAngles = value.transform.localEulerAngles + offSet;
    }

    public void SetToZero()
    {
        SetInitial();
        objectToRotate.transform.eulerAngles = Vector3.zero;
    }

    public void SetLocalToZero()
    {
        SetInitial();
        objectToRotate.transform.localEulerAngles = Vector3.zero;
    }

    public void SetLocalX(float value)
    {
        SetInitial();
        objectToRotate.transform.localEulerAngles = new Vector3(value, objectToRotate.transform.localEulerAngles.y, objectToRotate.transform.localEulerAngles.z);
    }

    public void SetLocalY(float value)
    {
        SetInitial();
        objectToRotate.transform.localEulerAngles = new Vector3(objectToRotate.transform.localEulerAngles.x, value, objectToRotate.transform.localEulerAngles.z);
    }

    public void SetLocalZ(float value)
    {
        SetInitial();
        objectToRotate.transform.localEulerAngles = new Vector3(objectToRotate.transform.localEulerAngles.x, objectToRotate.transform.localEulerAngles.y, value);
    }

    public void SetToInitial()
    {
        objectToRotate.transform.eulerAngles = initialEulerAngles;
    }
}
