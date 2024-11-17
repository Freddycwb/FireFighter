using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSetter : MonoBehaviour
{
    [SerializeField] private Transform objToSetPos;

    private void OnEnable()
    {
        if (objToSetPos == null)
        {
            objToSetPos = gameObject.transform;
        }
    }

    public void SetPosition(GameObject value)
    {
        if (value != null)
        {
            objToSetPos.transform.position = value.transform.position;
        }
    }

    public void SetPosition(GameObjectVariable value)
    {
        if (value != null && value.Value != null)
        {
            objToSetPos.transform.position = value.Value.transform.position;
        }
    }
}
