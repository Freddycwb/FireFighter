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
        transform.localPosition = Vector3.zero;
    }
    public void SetLocalRotationToZero()
    {
        transform.localEulerAngles = Vector3.zero;
    }
    public void SetLocalScaleToOne()
    {
        transform.localScale = Vector3.one;
    }
}
