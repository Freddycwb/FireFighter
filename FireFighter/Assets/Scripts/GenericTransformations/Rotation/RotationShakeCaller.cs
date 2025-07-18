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

    public void SetCounter(InvokeAfterCounter value)
    {
        rotationShake.CallShakeByCounter(value, new Vector3(time, intensity, delayBetweenShake));
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
