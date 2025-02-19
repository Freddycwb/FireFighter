using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObjectVariable targetVariable;

    [SerializeField] private Vector3 offSet;

    [SerializeField] private float targetYOffset;

    public void SetTarget(GameObject value)
    {
        target = value;
    }

    public void SetTarget(GameObjectVariable value)
    {
        target = value.Value;
    }

    public void SetOffset(Vector3 value)
    {
        offSet = value;
    }

    private void Start()
    {
        if (targetVariable != null)
        {
            target = targetVariable.Value;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        obj.transform.rotation = Quaternion.LookRotation((target.transform.position + Vector3.up * targetYOffset) - obj.transform.position, Vector3.up);
        obj.transform.eulerAngles += offSet;
    }
}
