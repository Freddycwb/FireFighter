using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBetweenTwoValues : MonoBehaviour
{
    [SerializeField] private GameObject objectToScale;
    [System.Flags]
    public enum Axis
    {
        None = 0,
        x = 1,
        y = 2,
        z = 4
    }
    [SerializeField] private Axis axis;
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

    public void SetCurrentValueToMax()
    {
        SetCurrentValue(minCurrentMaxValue.z);
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
        minCurrentMaxValue.z = value.GetMaxValue();
    }

    public void SetTimer(InvokeAfterTimer value)
    {
        if (value == null)
        {
            return;
        }
        timer = value;
    }

    public void SetTimerToNull()
    {
        if (timer == null)
        {
            return;
        }
        timer = null;
    }

    public void SetScale()
    {
        float perc = minCurrentMaxValue.y / (minCurrentMaxValue.z - minCurrentMaxValue.x);
        float minToMaxScale = minMaxScale.y - minMaxScale.x;

        float currentExtraScale = minToMaxScale * perc;

        float currentScale = currentExtraScale + minMaxScale.x;

        if (!(currentScale > 0 || currentScale < 0))
        {
            return;
        }

        if (!((axis & Axis.x) == 0))
        {
            objectToScale.transform.localScale = new Vector3(currentScale, objectToScale.transform.localScale.y, objectToScale.transform.localScale.z);
        }
        if (!((axis & Axis.y) == 0))
        {
            objectToScale.transform.localScale = new Vector3(objectToScale.transform.localScale.x, currentScale, objectToScale.transform.localScale.z);
        }
        if (!((axis & Axis.z) == 0))
        {
            objectToScale.transform.localScale = new Vector3(objectToScale.transform.localScale.x, objectToScale.transform.localScale.y, currentScale);
        }
    }
}
