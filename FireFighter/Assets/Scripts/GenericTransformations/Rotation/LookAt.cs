using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObjectVariable targetVarible;

    [SerializeField] private float yOffset;

    private void OnEnable()
    {
        if (targetVarible != null)
        {
            target = targetVarible.Value;
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
