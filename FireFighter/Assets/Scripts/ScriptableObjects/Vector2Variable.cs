using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Vector2Variable : Variable<Vector2>
{
    public void SetValueToZero()
    {
        Value = Vector2.zero;
    }

    public void SetValueByGameObjectRotation(GameObject value)
    {
        Value = new Vector2(value.transform.eulerAngles.x, value.transform.eulerAngles.y);
    }
}
