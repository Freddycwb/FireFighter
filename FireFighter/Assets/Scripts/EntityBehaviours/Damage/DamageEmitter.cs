using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageEmitter : MonoBehaviour
{
    [SerializeField] private CollisionType.Types damageType;
    [SerializeField] private float damageValue = 1;
    [SerializeField] private FloatVariable damageValueVariable;
    [SerializeField] private Vector2 knockbackForce;
    [SerializeField] private Vector2Variable knockbackForceVariable;

    public Action onEmitDamage;

    private void Start()
    {
        if (damageValueVariable != null)
        {
            damageValue = damageValueVariable.Value;
        }
        if (knockbackForceVariable != null)
        {
            knockbackForce = knockbackForceVariable.Value;
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

    public Vector2 GetKnockbackForceValue()
    {
        return knockbackForce;
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

    public void SetKnockbackForceValue(Vector2Variable value)
    {
        knockbackForce = value.Value;
    }
}
