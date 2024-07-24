using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private PoolInstantiator instantiator;

    public void SetInstantiator(PoolInstantiator value)
    {
        instantiator = value;
    }

    public PoolInstantiator GetInstantiator()
    {
        return instantiator;
    }

    public void Delete()
    {
        if (instantiator != null)
        {
            transform.SetParent(instantiator.transform);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
