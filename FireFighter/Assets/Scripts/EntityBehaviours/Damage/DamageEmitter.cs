using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageEmitter : MonoBehaviour
{
    [SerializeField] private CollisionType.Types damageType;
    [SerializeField] private float damageValue = 1;

    public Action onEmitDamage;

    public CollisionType.Types GetDamageType()
    {
        return damageType;
    }

    public float GetDamageValue()
    {
        if (onEmitDamage != null)
        {
            onEmitDamage.Invoke();
        }
        return damageValue;
    }
}
