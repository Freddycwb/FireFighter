using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentTransformations : MonoBehaviour
{
    public void SetParentToNull()
    {
        transform.SetParent(null);
    }
    public void SetParentToNull(Transform value)
    {
        value.SetParent(null);
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
