using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionShakeCaller : MonoBehaviour
{
    [SerializeField] private PositionShake positionShake;

    [SerializeField] private float time = 0.2f;
    [SerializeField] private float intensity = 0.3f;
    [SerializeField] private float delayBetweenShake = 0.01f;

    private float timeMultiplier = 1;
    private float intensityMultiplier = 1;
    private float delayBetweenShakeMultiplier = 1;

    public void CallShake()
    {
        if (time == float.NegativeInfinity || time == float.PositiveInfinity)
        {
            return;
        }
        positionShake.CallShake(new Vector3(time * timeMultiplier, intensity * intensityMultiplier, delayBetweenShake * delayBetweenShakeMultiplier));
    }

    public void SetTime(InvokeAfterTimer value)
    {
        time = value.GetCurrentTimeToAction();
    }

    public void SetIntensityMultiplier(float value)
    {
        intensityMultiplier = value;
    }

    public void SetCounter(InvokeAfterCounter value)
    {
        positionShake.CallShakeByCounter(value, new Vector3(time, intensity, delayBetweenShake));
    }

    public void SetCounter(GameObject value)
    {
        InvokeAfterCounter counter = value.GetComponent<InvokeAfterCounter>();
        SetCounter(counter);
    }

    public void SetCounter(GameObjectVariable value)
    {
        GameObject obj = value.Value;
        SetCounter(obj);
    }
}
