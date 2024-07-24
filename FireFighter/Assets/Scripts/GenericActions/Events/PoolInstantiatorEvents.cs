using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolInstantiatorEvents : MonoBehaviour
{
    [SerializeField] private PoolInstantiator poolInstantiator;

    [SerializeField] private UnityEvent<GameObject> onObjCreated;

    private bool listening;

    private void OnEnable()
    {
        if (poolInstantiator != null)
        {
            poolInstantiator.onObjCreated += OnObjCreated;
            listening = true;
        }
    }

    void OnObjCreated(GameObject value)
    {
        onObjCreated.Invoke(value);
    }

    private void OnDisable()
    {
        if (poolInstantiator != null)
        {
            poolInstantiator.onObjCreated -= OnObjCreated;
            listening = true;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
