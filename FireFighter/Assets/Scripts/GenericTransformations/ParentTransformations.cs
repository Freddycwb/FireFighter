using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentTransformations : MonoBehaviour
{
    [SerializeField] private Transform objToTransform;

    private void Awake()
    {
        if (objToTransform == null)
        {
            objToTransform = transform;
        }
    }

    private void Start()
    {
        if (objToTransform == null)
        {
            objToTransform = transform;
        }
    }

    public void SetParentToNull()
    {
        if (objToTransform == null)
        {
            objToTransform = transform;
        }
        objToTransform.SetParent(null);
    }
    public void SetParentToNull(Transform value)
    {
        value.SetParent(null);
    }

    public void SetParentToObjectInHolder(GameObjectHolder value)
    {
        objToTransform.SetParent(value.GetGameObject().transform);
    }


    public void SetLocalPositionToZero()
    {
        objToTransform.transform.localPosition = Vector3.zero;
    }

    public void SetLocalYPosition(float value)
    {
        objToTransform.transform.localPosition = new Vector3(objToTransform.transform.localPosition.x, value, objToTransform.transform.localPosition.z);
    }


    public void SetLocalRotationToZero()
    {
        objToTransform.transform.localEulerAngles = Vector3.zero;
    }


    public void SetLocalScaleToOne()
    {
        objToTransform.transform.localScale = Vector3.one;
    }
}
