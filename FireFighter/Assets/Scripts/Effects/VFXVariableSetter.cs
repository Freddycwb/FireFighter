using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXVariableSetter : MonoBehaviour
{
    [SerializeField] private VisualEffect vfx;
    [SerializeField] private string variableID;

    public void SetFloat(float value)
    {
        vfx.SetFloat(variableID, value);
    }

    public void SetFloat(FloatVariable value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetFloat(variableID, value.Value);
    }

    public void SetFloatByGameObjectScale(GameObject value)
    {
        vfx.SetFloat(variableID, value.transform.localScale.x);
    }





    public void SetVector2X(float value)
    {
        vfx.SetVector2(variableID, new Vector2(value, vfx.GetVector2(variableID).y));
    }

    public void SetVector2X(FloatVariable value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector2(variableID, new Vector2(value.Value, vfx.GetVector2(variableID).y));
    }

    public void SetVector2X(InvokeAfterCounter value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector2(variableID, new Vector2(value.GetCurrentValue(), vfx.GetVector2(variableID).y));
    }



    public void SetVector2Y(float value)
    {
        vfx.SetVector2(variableID, new Vector2(vfx.GetVector2(variableID).x, value));
    }

    public void SetVector2Y(FloatVariable value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector2(variableID, new Vector2(vfx.GetVector2(variableID).x, value.Value));
    }

    public void SetVector2Y(InvokeAfterCounter value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector2(variableID, new Vector2(vfx.GetVector2(variableID).x, value.GetCurrentValue()));
    }





    public void SetVector3X(float value)
    {
        vfx.SetVector3(variableID, new Vector3(value, vfx.GetVector3(variableID).y, vfx.GetVector3(variableID).z));
    }

    public void SetVector3X(FloatVariable value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector3(variableID, new Vector3(value.Value, vfx.GetVector3(variableID).y, vfx.GetVector3(variableID).z));
    }

    public void SetVector3X(InvokeAfterCounter value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector3(variableID, new Vector3(value.GetCurrentValue(), vfx.GetVector3(variableID).y, vfx.GetVector3(variableID).z));
    }

    public void SetVector3X(Renderer value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector3(variableID, new Vector3(value.bounds.size.x, vfx.GetVector3(variableID).y, vfx.GetVector3(variableID).z));
    }



    public void SetVector3Y(float value)
    {
        vfx.SetVector3(variableID, new Vector3(vfx.GetVector3(variableID).x, value, vfx.GetVector3(variableID).z));
    }

    public void SetVector3Y(FloatVariable value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector3(variableID, new Vector3(vfx.GetVector3(variableID).x, value.Value, vfx.GetVector3(variableID).z));
    }

    public void SetVector3Y(InvokeAfterCounter value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector3(variableID, new Vector3(vfx.GetVector3(variableID).x, value.GetCurrentValue(), vfx.GetVector3(variableID).z));
    }

    public void SetVector3Y(Renderer value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector3(variableID, new Vector3(vfx.GetVector3(variableID).x, value.bounds.size.y, vfx.GetVector3(variableID).z));
    }



    public void SetVector3Z(float value)
    {
        vfx.SetVector3(variableID, new Vector3(vfx.GetVector3(variableID).x, vfx.GetVector3(variableID).y, value));
    }

    public void SetVector3Z(FloatVariable value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector3(variableID, new Vector3(vfx.GetVector3(variableID).x, vfx.GetVector3(variableID).y, value.Value));
    }

    public void SetVector3Z(InvokeAfterCounter value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector3(variableID, new Vector3(vfx.GetVector3(variableID).x, vfx.GetVector3(variableID).y, value.GetCurrentValue()));
    }

    public void SetVector3Z(Renderer value)
    {
        if (value == null)
        {
            return;
        }
        vfx.SetVector3(variableID, new Vector3(vfx.GetVector3(variableID).x, vfx.GetVector3(variableID).y, value.bounds.size.z));
    }
}
