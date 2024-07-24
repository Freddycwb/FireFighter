using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInstantiator : MonoBehaviour
{
    [SerializeField] private int instancesOnStart;
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform parent;

    public Action<GameObject> onObjCreated;

    private void Start()
    {
        for (int i = 0; i < instancesOnStart; i++)
        {
            GameObject a = Instantiate(obj, transform.position, transform.rotation);
            a.transform.SetParent(transform);
            a.SetActive(false);
            a.AddComponent<PoolObject>().SetInstantiator(this);
        }
    }

    public void CreateObject()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        Transform t = spawnPoint != null ? spawnPoint : transform;

        GameObject a;

        if (transform.childCount > 0)
        {
            a = transform.GetChild(0).gameObject;
            a.transform.position = t.position;
            a.transform.rotation = t.rotation;
            a.transform.SetParent(parent);
            a.SetActive(true);
        }
        else
        {
            a = Instantiate(obj, t.position, t.rotation);
            a.transform.SetParent(parent);
            a.AddComponent<PoolObject>().SetInstantiator(this);
        }

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

        GameObject a;

        if (transform.childCount > 0)
        {
            a = transform.GetChild(0).gameObject;
            a.transform.position = value.position;
            a.transform.rotation = value.rotation;
            a.transform.SetParent(parent);
            a.SetActive(true);
        }
        else
        {
            a = Instantiate(obj, value.position, value.rotation);
            a.transform.SetParent(parent);
            a.AddComponent<PoolObject>().SetInstantiator(this);
        }

        if (onObjCreated != null)
        {
            onObjCreated.Invoke(a);
        }
    }
}
