using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyerEvents : MonoBehaviour
{
    [SerializeField] private Destroyer destroyer;

    [SerializeField] private UnityEvent onStartDelay;
    [SerializeField] private UnityEvent onDelete;

    private bool listening;

    private void OnEnable()
    {
        if (destroyer != null)
        {
            destroyer.onStartDelay += OnStartDelay;
            destroyer.onDelete += OnDelete;
            listening = true;
        }
    }

    void OnStartDelay(Destroyer value)
    {
        if (enabled)
        {
            onStartDelay.Invoke();
        }
    }

    void OnDelete(Destroyer value)
    {
        if (enabled)
        {
            onDelete.Invoke();
        }
    }

    private void OnDisable()
    {
        if (onDelete != null && listening)
        {
            destroyer.onStartDelay -= OnStartDelay;
            destroyer.onDelete -= OnDelete;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
