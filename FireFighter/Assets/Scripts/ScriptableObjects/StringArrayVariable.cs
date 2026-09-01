using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StringArrayVariable : Variable<string[]>
{
    public void SetValue(StringArrayVariable value)
    {
        Value = value.Value;
    }

    public void SetValueToNone()
    {
        Value = new string[0];
    }
}
