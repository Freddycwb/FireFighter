using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBetweenTwoValues : MonoBehaviour
{
    [SerializeField] private GameObject objectToScale;
    [SerializeField] private Vector2 minMaxScale;
    [SerializeField] private Vector2 currentMaxValue;

    public void SetCurrentValue(float value)
    {
        currentMaxValue.x = value;
    }

    public void SetCurrentValue(FloatVariable value)
    {
        currentMaxValue.x = value.Value;
    }

    public void SetCurrentValue(InvokeAfterCounter value)
    {
        currentMaxValue.x = value.GetCurrentValue();
    }

    public void SetMaxValue(float value)
    {
        currentMaxValue.y = value;
    }

    public void SetMaxValue(FloatVariable value)
    {
        currentMaxValue.y = value.Value;
    }

    public void SetMaxValue(InvokeAfterCounter value)
    {
        currentMaxValue.y = value.GetCurrentValue();
    }

    public void SetScale()
    {
        float perc = currentMaxValue.x / currentMaxValue.y;
        float minToMaxScale = minMaxScale.y - minMaxScale.x;

        float currentExtraScale = minToMaxScale * perc;

        float currentScale = currentExtraScale + minMaxScale.x;

        objectToScale.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }
}
