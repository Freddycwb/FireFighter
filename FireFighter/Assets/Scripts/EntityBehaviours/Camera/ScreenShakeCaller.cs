using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeCaller : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private float intensity;
    [SerializeField] private float delayBetweenShake;

    [SerializeField] private GameEventVector3 screenShake;

    public void CallScreenShake()
    {
        screenShake.Raise(new Vector3(time, intensity, delayBetweenShake));
    }
}
