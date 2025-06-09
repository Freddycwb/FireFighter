using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionShakeCaller : MonoBehaviour
{
    [SerializeField] private PositionShake positionShake;

    [SerializeField] private float time = 0.2f;
    [SerializeField] private float intensity = 0.3f;
    [SerializeField] private float delayBetweenShake = 0.01f;

    public void CallShake()
    {
        if (time == float.NegativeInfinity || time == float.PositiveInfinity)
        {
            return;
        }
        positionShake.CallShake(new Vector3(time, intensity, delayBetweenShake));
    }

    public void SetTime(InvokeAfterTimer value)
    {
        time = value.GetCurrentTimeToAction();
    }
}
