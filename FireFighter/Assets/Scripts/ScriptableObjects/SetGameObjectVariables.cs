using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectVariables : MonoBehaviour
{
    [SerializeField] private GameObjectVariable scriptableObject;
    [SerializeField] private GameObject value;
    [SerializeField] private bool setOnAwake = true;

    private void Awake()
    {
        if (setOnAwake)
        {
            scriptableObject.Value = value;
        }
    }

    public void SetGameObjectVariable()
    {
        scriptableObject.Value = value;
    }
}
