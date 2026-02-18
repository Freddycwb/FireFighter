using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using static UnityEngine.Rendering.DebugUI;

public class DamageChecker : MonoBehaviour
{
    [SerializeField] private DamageType.Types damageType;

    [SerializeField] private UnityEvent<float> takeDamage;

    [SerializeField] private SpecialDamageEventType.Types specialDamageEventType;
    [SerializeField] private bool alertEmitter = true;
    [SerializeField] private bool ignoreEmitterDisable;

    private List<GameObject> damageReceived = new List<GameObject>();
    private Coroutine damageReceiveRoutine;

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

    private void OnEnable()
    {
        damageReceived.Clear();
    }

    public void CheckDamage(GameObject value)
    {
        if (!enabled)
        {
            return;
        }
        DamageEmitter emitter = null;
        if (value.GetComponent<GameObjectHolder>() != null)
        {
            emitter = value.GetComponent<GameObjectHolder>().GetGameObject().GetComponent<DamageEmitter>();
        }
        else if (value.transform.parent != null)
        {
            emitter = value.transform.parent.GetComponent<DamageEmitter>();
        }
        ReceiveDamage(emitter);
    }

    public void ReceiveDamage(DamageEmitter emitter)
    {
        if (!enabled || emitter == null)
        {
            return;
        }
        if (damageReceived.Contains(emitter.gameObject) || ((!emitter.enabled || !emitter.gameObject.activeSelf || !emitter.gameObject.activeInHierarchy) && !ignoreEmitterDisable))
        {
            return;
        }
        if ((emitter.GetDamageType() & damageType) != 0)
        {
            lastDamage = emitter.GetDamageValue(alertEmitter, specialDamageEventType);
            lastKnockbackForce = emitter.GetKnockbackForceValue();
            if (onTakeDamage != null)
            {
                onTakeDamage.Invoke();
            }
            takeDamage.Invoke(-lastDamage);
            damageReceived.Add(emitter.gameObject);
            if (enabled && gameObject.activeSelf && gameObject.activeInHierarchy)
            {
                StartCoroutine(RemoveDamageReceived(emitter.gameObject));
            }
        }
    }

    private IEnumerator RemoveDamageReceived(GameObject value)
    {
        yield return new WaitForSeconds(0.12f);
        damageReceived.Remove(value);
    }

    private void OnDisable()
    {
        damageReceived.Clear();
        StopAllCoroutines();
    }
}
