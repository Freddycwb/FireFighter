using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DamageChecker : MonoBehaviour
{
    [SerializeField] private CollisionType.Types damageType;

    [SerializeField] private UnityEvent<float> takeDamage;

    [SerializeField] private bool alertEmitter = true;

    private float lastDamage;
    private Vector2 lastKnockbackForce;

    public Action onTakeDamage; 

    public float GetLastDamage()
    {
        return lastDamage;
    }

    public Vector2 GetLastKnockbackForce()
    {
        return lastKnockbackForce;
    }

    public void CheckDamage(GameObject value)
    {
        if (!enabled)
        {
            return;
        }
        DamageEmitter emitter = null;
        if (value.transform.parent.GetComponent<DamageEmitter>() != null)
        {
            emitter = value.transform.parent.GetComponent<DamageEmitter>();
        }
        if ((emitter.GetDamageType() & damageType) != 0)
        {
            lastDamage = emitter.GetDamageValue(alertEmitter);
            lastKnockbackForce = emitter.GetKnockbackForceValue();
            if (onTakeDamage != null)
            {
                onTakeDamage.Invoke();
            }
            takeDamage.Invoke(-lastDamage);
        }
    }
}
