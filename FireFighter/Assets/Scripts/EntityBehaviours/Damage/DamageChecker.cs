using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using static UnityEngine.Rendering.DebugUI;

public class DamageChecker : MonoBehaviour
{
    [SerializeField] private CollisionType.Types damageType;

    [SerializeField] private UnityEvent<float> takeDamage;

    [SerializeField] private bool alertEmitter = true;

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
        if (value.transform.parent != null)
        {
            emitter = value.transform.parent.GetComponent<DamageEmitter>();
        }
        ReceiveDamage(emitter);
    }

    public void ReceiveDamage(DamageEmitter emitter)
    {
        if (emitter == null)
        {
            return;
        }
        if (damageReceived.Contains(emitter.gameObject))
        {
            return;
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
            damageReceived.Add(emitter.gameObject);
            if (enabled && gameObject.activeSelf)
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
}
