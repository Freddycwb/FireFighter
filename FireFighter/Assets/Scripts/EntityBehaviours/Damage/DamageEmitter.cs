using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageEmitter : MonoBehaviour
{
    [SerializeField] private CollisionType.Types damageType;
    [SerializeField] private float damageValue = 1;
    [SerializeField] private FloatVariable damageValueVariable;

    public Action onEmitDamage;

    private void Start()
    {
        if (damageValueVariable != null)
        {
            damageValue = damageValueVariable.Value;
        }
    }

    public CollisionType.Types GetDamageType()
    {
        return damageType;
    }

    public float GetDamageValue(bool value)
    {
        if (onEmitDamage != null && value)
        {
            onEmitDamage.Invoke();
        }
        return damageValue;
    }

    public void SetDamageValue(float value)
    {
        damageValue = value;
    }

    public void SetDamageValue(FloatVariable value)
    {
        damageValue = value.Value;
    }

    public void SetDamageValue(InvokeAfterCounter value)
    {
        damageValue = value.GetCurrentValue();
    }
}
