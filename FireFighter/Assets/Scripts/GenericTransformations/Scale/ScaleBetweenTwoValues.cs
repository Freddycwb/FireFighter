using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBetweenTwoValues : MonoBehaviour
{
    [SerializeField] private GameObject objectToScale;
    [SerializeField] private Vector2 minMaxScale;
    [SerializeField] private Vector3 minCurrentMaxValue;

    [SerializeField] private bool setScaleOnSetCurrent = true;
    private InvokeAfterTimer timer;

    private void Update()
    {
        if (timer != null)
        {
            minCurrentMaxValue.y = Mathf.Clamp(timer.GetCurrentTimePass() / timer.GetCurrentTimeToAction() * minCurrentMaxValue.z, minCurrentMaxValue.x, minCurrentMaxValue.z);
            SetScale();
        }
    }

    public void SetCurrentValue(float value)
    {
        minCurrentMaxValue.y = Mathf.Clamp(value, minCurrentMaxValue.x, minCurrentMaxValue.z);
        minCurrentMaxValue.y -= minCurrentMaxValue.x;
        if (setScaleOnSetCurrent)
        {
            SetScale();
        }
    }

    public void SetCurrentValue(FloatVariable value)
    {
        if (value == null)
        {
            return;
        }
        SetCurrentValue(value.Value);
    }

    public void SetCurrentValue(InvokeAfterCounter value)
    {
        if (value == null)
        {
            return;
        }
        SetCurrentValue(value.GetCurrentValue());
    }

    public void SetMaxValue(float value)
    {
        minCurrentMaxValue.z = value;
    }

    public void SetMaxValue(FloatVariable value)
    {
        if (value == null)
        {
            return;
        }
        minCurrentMaxValue.z = value.Value;
    }

    public void SetMaxValue(InvokeAfterCounter value)
    {
        if (value == null)
        {
            return;
        }
        minCurrentMaxValue.z = value.GetCurrentValue();
    }

    public void SetTimer(InvokeAfterTimer value)
    {
        if (value == null)
        {
            return;
        }
        timer = value;
    }

    public void SetScale()
    {
        float perc = minCurrentMaxValue.y / (minCurrentMaxValue.z - minCurrentMaxValue.x);
        float minToMaxScale = minMaxScale.y - minMaxScale.x;

        float currentExtraScale = minToMaxScale * perc;

        float currentScale = currentExtraScale + minMaxScale.x;

        objectToScale.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }
}
