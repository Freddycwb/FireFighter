using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersistBetweenScenesEvents : MonoBehaviour
{
    [SerializeField] private PersistBetweenScenes persistBetweenScenes;

    [SerializeField] private UnityEvent onObjDontDestroy;

    private bool listening;

    private void OnEnable()
    {
        if (persistBetweenScenes != null)
        {
            persistBetweenScenes.onObjDontDestroy += OnObjDontDestroy;
            listening = true;
        }
    }

    void OnObjDontDestroy()
    {
        if (enabled)
        {
            onObjDontDestroy.Invoke();
        }
    }

    private void OnDisable()
    {
        if (persistBetweenScenes != null && listening)
        {
            persistBetweenScenes.onObjDontDestroy -= OnObjDontDestroy;
            listening = true;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
