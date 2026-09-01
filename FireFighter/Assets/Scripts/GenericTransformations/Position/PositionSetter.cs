using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PositionSetter : MonoBehaviour
{
    [SerializeField] private Transform objToSetPos;
    [SerializeField] private GameObjectVariable objToSetPosVariable;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject reference;

    private void OnEnable()
    {
        if (objToSetPos == null)
        {
            if (objToSetPosVariable != null)
            {
                objToSetPos = objToSetPosVariable.Value.transform;
            }
            else
            {
                objToSetPos = gameObject.transform;
            }
        }
    }

    public void SetReference(GameObjectVariable value)
    {
        SetReference(value.Value);
    }

    public void SetReference(GameObject value)
    {
        reference = value;
    }

    public void SetPosition(Vector3 value)
    {
        if (value != null)
        {
            objToSetPos.transform.position = value + offset;
        }
    }

    public void SetPosition(GameObject value)
    {
        if (value != null)
        {
            SetPosition(value.transform.position);
        }
    }

    public void SetPosition(GameObjectHolder value)
    {
        if (value != null)
        {
            SetPosition(value.GetGameObject());
        }
    }

    public void SetPosition(GameObjectVariable value)
    {
        if (value != null && value.Value != null)
        {
            SetPosition(value.Value.transform.position);
        }
    }

    public void SetPositionY(GameObject value)
    {
        if (value != null)
        {
            Vector3 pos = new Vector3(objToSetPos.transform.position.x, value.transform.position.y, objToSetPos.transform.position.z);
            SetPosition(pos);
        }
    }

    void Update()
    {
        if (reference != null)
        {
            SetPosition(reference);
        }
    }
}

