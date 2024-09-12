using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageChecker : MonoBehaviour
{
    [SerializeField] private DamageType.Types damageType;

    [SerializeField] private UnityEvent<int> takeDamage;

    public void CheckDamage(GameObject value)
    {
        if (!enabled)
        {
            return;
        }
        DamageEmitter emitter = null;
        if (value.GetComponent<DamageEmitter>() != null)
        {
            emitter = value.GetComponent<DamageEmitter>();
        }
        if ((emitter.GetDamageType() & damageType) != 0)
        {
            takeDamage.Invoke(emitter.GetDamageValue());
        }
    }
}
