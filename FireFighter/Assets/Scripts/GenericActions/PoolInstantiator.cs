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
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private bool destroySurplus;

    private List<GameObject> instances = new List<GameObject>();

    private Vector3 newPosition;
    private Quaternion newRotation;

    public Action<GameObject> onObjCreated;

    private void Start()
    {
        newPosition.x = float.PositiveInfinity;
        newRotation.x = float.PositiveInfinity;

        for (int i = 0; i < instancesOnStart; i++)
        {
            GameObject a = Instantiate(obj, transform.position, transform.rotation);
            a.transform.SetParent(transform);
            a.SetActive(false);
            a.AddComponent<PoolObject>().SetInstantiator(this);
        }
    }

    private void SetNewCoordinates()
    {
        if (newPosition.x == float.PositiveInfinity)
        {
            if (spawnPoint != null)
            {
                newPosition = spawnPoint.position;
            }
            else
            {
                newPosition = transform.position;
            }
        }

        if (newRotation.x == float.PositiveInfinity)
        {
            if (spawnPoint != null)
            {
                newRotation = spawnPoint.rotation;
            }
            else
            {
                newRotation = transform.rotation;
            }
        }

        newPosition += positionOffset;
    }

    public void CreateObject()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        SetNewCoordinates();

        GameObject a;

        if (transform.childCount > 0)
        {
            a = transform.GetChild(0).gameObject;
            a.transform.position = newPosition;
            a.transform.rotation = newRotation;
            a.transform.SetParent(parent);
            a.SetActive(true);
            instances.Add(a);
        }
        else
        {
            a = Instantiate(obj, newPosition, newRotation);
            a.transform.SetParent(parent);
            a.AddComponent<PoolObject>().SetInstantiator(this);
            instances.Add(a);
        }

        if (destroySurplus)
        {
            while (instancesOnStart < instances.Count)
            {
                GameObject b = instances[0];
                if (b.GetComponent<Destroyer>() != null)
                {
                    b.GetComponent<Destroyer>().Delete();
                }
                instances.Remove(b);
            }
        }

        if (onObjCreated != null)
        {
            onObjCreated.Invoke(a);
        }

        newPosition.x = float.PositiveInfinity;
        newRotation.x = float.PositiveInfinity;
    }

    public void CreateObject(Transform value)
    {
        newPosition = value.position;
        newRotation = value.rotation;
        CreateObject();
    }

    public void CreateObjectInPosition(Vector3 value)
    {
        newPosition = value;
        CreateObject();
    }
}
