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
        vfx.SetFloat(variableID, value.Value);
    }

    public void SetVector2X(float value)
    {
        vfx.SetVector2(variableID, new Vector2(value, vfx.GetVector2(variableID).y));
    }

    public void SetVector2X(FloatVariable value)
    {
        vfx.SetVector2(variableID, new Vector2(value.Value, vfx.GetVector2(variableID).y));
    }

    public void SetVector2X(InvokeAfterCounter value)
    {
        vfx.SetVector2(variableID, new Vector2(value.GetCurrentValue(), vfx.GetVector2(variableID).y));
    }

    public void SetVector2Y(float value)
    {
        vfx.SetVector2(variableID, new Vector2(vfx.GetVector2(variableID).x, value));
    }

    public void SetVector2Y(FloatVariable value)
    {
        vfx.SetVector2(variableID, new Vector2(vfx.GetVector2(variableID).x, value.Value));
    }

    public void SetVector2Y(InvokeAfterCounter value)
    {
        vfx.SetVector2(variableID, new Vector2(vfx.GetVector2(variableID).x, value.GetCurrentValue()));
    }
}