using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PositionSetter : MonoBehaviour
{
    [SerializeField] private Transform objToSetPos;
    [SerializeField] private GameObjectVariable objToSetPosVariable;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool localOffset;
    [SerializeField] private GameObject reference;

    private void OnEnable()
    {
        if (objToSetPos == null)
        {
            SetObjectToVariable();
        }
    }

    private void SetObjectToVariable()
    {
        if (objToSetPosVariable != null)
        {
            if (objToSetPosVariable.Value != null)
            {
                objToSetPos = objToSetPosVariable.Value.transform;
            }
        }
        else
        {
            objToSetPos = gameObject.transform;
        }
    }

    public void SetOffSetY(float value)
    {
        offset = new Vector3(offset.x, value, offset.z);
    }

    public void SetOffSetYByScale(GameObject value)
    {
        offset = new Vector3(offset.x, (value.transform.localScale.x + value.transform.localScale.y + value.transform.localScale.z) / 3, offset.z);
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
        if (value == null) return;

        if (objToSetPos == null)
        {
            SetObjectToVariable();
        }

        if (localOffset)
        {
            objToSetPos.transform.position = value;
            objToSetPos.transform.localPosition = new Vector3(objToSetPos.transform.localPosition.x + offset.x,
                objToSetPos.transform.localPosition.y + offset.y, objToSetPos.transform.localPosition.z + offset.z);
        }
        else
        {
            objToSetPos.transform.position = value + offset;
        }
    }

    public void SetPosition(Vector3Variable value)
    {
        if (value != null)
        {
            SetPosition(value.Value);
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
        if (value == null) return;

        if (objToSetPos == null)
        {
            SetObjectToVariable();
        }

        Vector3 pos = new Vector3(objToSetPos.transform.position.x, value.transform.position.y, objToSetPos.transform.position.z);
        SetPosition(pos);
    }

    void Update()
    {
        if (reference != null)
        {
            SetPosition(reference);
        }
    }
}

