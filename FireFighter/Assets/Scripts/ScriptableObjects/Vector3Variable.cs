using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Vector3Variable : Variable<Vector3>
{
    public void SetValueToZero()
    {
        Value = Vector3.zero;
    }

    public void SetValueByGameObjectPosition(GameObject value)
    {
        Value = value.transform.position;
    }
}
