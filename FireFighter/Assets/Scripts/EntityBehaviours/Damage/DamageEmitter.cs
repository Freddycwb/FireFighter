using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEmitter : MonoBehaviour
{
    [SerializeField] private DamageType.Types damageType;
    [SerializeField] private int damageValue = 1;

    public DamageType.Types GetDamageType()
    {
        return damageType;
    }

    public int GetDamageValue()
    {
        return damageValue;
    }
}
