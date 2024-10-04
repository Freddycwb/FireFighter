using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageCheckerEvents : MonoBehaviour
{
    [SerializeField] private DamageChecker damageChecker;

    [SerializeField] private UnityEvent onTakeDamage;

    private bool listening;

    private void OnEnable()
    {
        if (damageChecker != null)
        {
            damageChecker.onTakeDamage += OnTakeDamage;
            listening = true;
        }
    }

    void OnTakeDamage()
    {
        if (enabled)
        {
            onTakeDamage.Invoke();
        }
    }

    private void OnDisable()
    {
        if (damageChecker != null && listening)
        {
            damageChecker.onTakeDamage -= OnTakeDamage;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
