using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageEmitterEvents : MonoBehaviour
{
    [SerializeField] private DamageEmitter damageEmitter;

    [SerializeField] private UnityEvent onEmitDamage;

    private bool listening;

    private void OnEnable()
    {
        if (damageEmitter != null)
        {
            damageEmitter.onEmitDamage += OnEmitteDamage;
            listening = true;
        }
    }

    void OnEmitteDamage()
    {
        if (enabled)
        {
            onEmitDamage.Invoke();
        }
    }

    private void OnDisable()
    {
        if (damageEmitter != null && listening)
        {
            damageEmitter.onEmitDamage -= OnEmitteDamage;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
