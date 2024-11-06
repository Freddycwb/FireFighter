using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyerEvents : MonoBehaviour
{
    [SerializeField] private Destroyer destroyer;

    [SerializeField] private UnityEvent onDelete;

    private bool listening;

    private void OnEnable()
    {
        if (destroyer != null)
        {
            destroyer.onDelete += OnDelete;
            listening = true;
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
            destroyer.onDelete -= OnDelete;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
