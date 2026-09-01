using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PositionSetterBetweenObjects : MonoBehaviour
{
    [SerializeField] private GameObject objToMove;
    [SerializeField] private List<GameObject> objs = new List<GameObject>();

    public void AddPoint(GameObjectVariable value)
    {
        AddPoint(value.Value);
    }

    public void AddPoint(GameObject value)
    {
        objs.Add(value);
    }

    public void SetPosition()
    {
        objToMove.transform.position = GetCenterOfPoints();
    }

    public Vector3 GetCenterOfPoints()
    {
        if (objs == null || objs.Count == 0) return Vector3.zero;

        Vector3 total = Vector3.zero;

        foreach (GameObject p in objs)
        {
            total += p.transform.position;
        }

        return total / objs.Count;
    }
}
