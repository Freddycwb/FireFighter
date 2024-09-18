using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageChecker : MonoBehaviour
{
    [SerializeField] private CollisionType.Types damageType;

    [SerializeField] private UnityEvent<float> takeDamage;

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
            takeDamage.Invoke(-emitter.GetDamageValue());
        }
    }
}
