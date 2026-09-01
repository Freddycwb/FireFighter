using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SetStringArrayVariables : MonoBehaviour
{
    [SerializeField] private StringArrayVariable scriptableObject;

    public void AddStringToArray(string value)
    {

        if (scriptableObject.Value.Length > 0)
        {
            string[] newValue = new string[scriptableObject.Value.Length + 1];
            int count = 0;
            foreach (string a in scriptableObject.Value)
            {
                newValue[count] = a;
                count++;
            }
            newValue[newValue.Length - 1] = value;
            scriptableObject.Value = newValue;
        }
        else
        {
            string[] newValue = new string[1];
            newValue[0] = value;
            scriptableObject.Value = newValue;
        }
    }

    public void AddStringToArray(StringHolder value)
    {
        AddStringToArray(value.GetString());
    }
}
