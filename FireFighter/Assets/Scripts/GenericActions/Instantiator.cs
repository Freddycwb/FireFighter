using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform parent;

    public Action<GameObject> onObjCreated;

    public void CreateObject()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        Transform t = spawnPoint != null ? spawnPoint : transform;
        GameObject a = Instantiate(obj, t.position, t.rotation);
        a.transform.SetParent(parent);
        if (onObjCreated != null)
        {
            onObjCreated.Invoke(a);
        }
    }

    public void CreateObject(Transform value)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        GameObject a = Instantiate(obj, value.position, value.rotation);
        a.transform.SetParent(parent);
        if (onObjCreated != null)
        {
            onObjCreated.Invoke(a);
        }
    }
}
