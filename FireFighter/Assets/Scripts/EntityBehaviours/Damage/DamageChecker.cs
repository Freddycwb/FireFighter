using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DamageChecker : MonoBehaviour
{
    [SerializeField] private CollisionType.Types damageType;

    [SerializeField] private UnityEvent<float> takeDamage;

    public Action onTakeDamage; 

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
            if (onTakeDamage != null)
            {
                onTakeDamage.Invoke();
            }
            takeDamage.Invoke(-emitter.GetDamageValue());
        }
    }
}
