using UnityEngine;

public class SetFMODEventParameter : MonoBehaviour
{
    [SerializeField] private FMODUnity.StudioEventEmitter emitter;
    [SerializeField] private string param;

    public void SetParameterByValue(float value)
    {
        emitter.SetParameter(param, value);
    }

    public void SetParameterByCurrentCounter(InvokeAfterCounter value)
    {
        SetParameterByValue((value.GetCurrentValue() - value.GetMinValue()) / (value.GetMaxValue() - value.GetMinValue()));
    }
}
