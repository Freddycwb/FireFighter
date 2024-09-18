using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObjectVariable targetVariable;

    [SerializeField] private float yOffset;

    public void SetTarget(GameObject value)
    {
        target = value;
    }

    public void SetTarget(GameObjectVariable value)
    {
        target = value.Value;
    }

    private void OnEnable()
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
        obj.transform.rotation = Quaternion.LookRotation((target.transform.position + Vector3.up * yOffset) - obj.transform.position, Vector3.up);
    }
}
