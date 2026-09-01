using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringHolder : MonoBehaviour
{
    [SerializeField] private string stringInsured;

    public string GetString()
    {
        return stringInsured;
    }
}
