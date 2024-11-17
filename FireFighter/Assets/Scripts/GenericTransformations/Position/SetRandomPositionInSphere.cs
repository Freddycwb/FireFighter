using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomPositionInSphere : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private float radius;

    [SerializeField] private bool setFromCurrentPos;

    private Vector3 _startPos;

    private void OnEnable()
    {
        if (obj == null) 
        { 
            obj = transform;
        }
        _startPos = obj.localPosition;
    }

    public void RandomisePosition()
    {
        if (setFromCurrentPos)
        {
            obj.localPosition = Random.insideUnitSphere * radius + _startPos;
        }
        else
        {
            obj.localPosition = Random.insideUnitSphere * radius;
        }
    }
}
