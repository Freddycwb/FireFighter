using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomPositionInSphere : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private float radius;

    public void RandomisePosition()
    {
        obj.localPosition = Random.insideUnitSphere * radius;
    }
}
