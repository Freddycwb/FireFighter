using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageEmitter : MonoBehaviour
{
    [SerializeField] private DamageType.Types damageType;
    [SerializeField] private float damageValue = 1;
    [SerializeField] private FloatVariable damageValueVariable;
    [SerializeField] private Vector2 knockbackForce;
    [SerializeField] private Vector2Variable knockbackForceVariable;

    private SpecialDamageEventType.Types _lastSpecialDamageEventType = SpecialDamageEventType.Types.None;
    public Action<SpecialDamageEventType.Types> onEmitDamage;

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

    public DamageType.Types GetDamageType()
    {
        return damageType;
    }

    public SpecialDamageEventType.Types GetLastSpecialDamageEventType()
    {
        return _lastSpecialDamageEventType;
    }

    public float GetDamageValue(bool alertEmitter, SpecialDamageEventType.Types specialDamageEventType)
    {
        _lastSpecialDamageEventType = specialDamageEventType;
        if (onEmitDamage != null && alertEmitter)
        {
            onEmitDamage.Invoke(specialDamageEventType);
        }
        return damageValue;
    }

    public Vector2 GetKnockbackForceValue()
    {
        return knockbackForce;
    }

    public void SetDamageType(DamageType.Types value)
    {
        damageType = value;
    }

    public void SetDamageType(int value)
    {
        damageType = (DamageType.Types)value;
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

    public void GiveDamageFromCollision(GameObject value)
    {
        if (value.GetComponent<GameObjectHolder>() != null)
        {
            GiveDamage(value.GetComponent<GameObjectHolder>().GetGameObject().GetComponent<DamageChecker>());
        }
        else if (value.transform.parent != null)
        {
            GiveDamage(value.transform.parent.GetComponent<DamageChecker>());
        }
    }

    public void GiveDamage(DamageChecker checker)
    {
        if (checker != null)
        {
            checker.ReceiveDamage(this);
        }
    }
}
