using UnityEngine;

public class AnimationCurveValue : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float fullValue;
    private InvokeAfterCounter counter;

    public void SetCounter(InvokeAfterCounter value)
    {
        counter = value;
    }

    public void SetFullValue(float value)
    {
        fullValue = value;
    }

    public void SetFullValue(InvokeAfterTimer value)
    {
        SetFullValue(value.GetTimeToAction());
    }

    public void SetFullValueByThrowerForceX(Thrower value)
    {
        SetFullValue(value.GetForceX());
    }

    public void SetFullValueByThrowerForceY(Thrower value)
    {
        SetFullValue(value.GetForceY());
    }

    public float GetValueByCounter()
    {
        return curve.Evaluate(counter.GetCurrentValue() / counter.GetMaxValue()) * fullValue;
    }
}
