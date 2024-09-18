using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstantiatorEvents : MonoBehaviour
{
    [SerializeField] private Instantiator instantiator;

    [SerializeField] private UnityEvent<GameObject> onObjCreated;

    private bool listening;

    private void OnEnable()
    {
        if (instantiator != null)
        {
            instantiator.onObjCreated += OnObjCreated;
            listening = true;
        }
    }

    void OnObjCreated(GameObject value)
    {
        if (enabled)
        {
            onObjCreated.Invoke(value);
        }
    }

    private void OnDisable()
    {
        if (instantiator != null && listening)
        {
            instantiator.onObjCreated -= OnObjCreated;
            listening = true;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
