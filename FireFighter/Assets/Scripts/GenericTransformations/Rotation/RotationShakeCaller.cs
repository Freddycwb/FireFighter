using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationShakeCaller : MonoBehaviour
{
    [SerializeField] private RotationShake rotationShake;

    [SerializeField] private float time = 0.2f;
    [SerializeField] private float intensity = 0.3f;
    [SerializeField] private float delayBetweenShake = 0.01f;

    public void CallShake()
    {
        rotationShake.CallShake(new Vector3(time, intensity, delayBetweenShake));
    }
}
