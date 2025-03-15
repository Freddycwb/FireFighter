using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterOverlapEvents : MonoBehaviour
{
    [SerializeField] private InvokeAfterOverlap invokeAfterOverlap;

    [SerializeField] private UnityEvent<GameObject> onContact;

    private bool listening;

    private void OnEnable()
    {
        if (invokeAfterOverlap != null && !listening)
        {
            invokeAfterOverlap.onContact += OnContact;
            listening = true;
        }
    }

    void OnContact(GameObject value)
    {
        if (enabled)
        {
            onContact.Invoke(value);
        }
    }
    private void OnDisable()
    {
        if (invokeAfterOverlap != null && listening)
        {
            invokeAfterOverlap.onContact -= OnContact;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
