using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ProBuilderScaleSetter : MonoBehaviour
{
    public void SetScaleX(Renderer value)
    {
        transform.localScale = new Vector3(value.bounds.size.x/2, transform.localScale.y, transform.localScale.z);
    }

    public void SetScaleY(Renderer value)
    {
        transform.localScale = new Vector3(transform.localScale.x, value.bounds.size.y/2, transform.localScale.z);
    }

    public void SetScaleZ(Renderer value)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value.bounds.size.z/2);
    }
}
