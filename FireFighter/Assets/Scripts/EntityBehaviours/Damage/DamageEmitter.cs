using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEmitter : MonoBehaviour
{
    [SerializeField] private CollisionType.Types damageType;
    [SerializeField] private float damageValue = 1;

    public CollisionType.Types GetDamageType()
    {
        return damageType;
    }

    public float GetDamageValue()
    {
        return damageValue;
    }
}