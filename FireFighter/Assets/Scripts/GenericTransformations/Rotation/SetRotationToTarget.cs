using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SetRotationToTarget : MonoBehaviour
{
    [SerializeField] private GameObject objToRotate;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObjectVariable targetVariable;

    [System.Flags]
    public enum Axis
    {
        None = 0,
        x = 1,
        y = 2,
        z = 4
    }
    [SerializeField] private Axis axis;

    private void Start()
    {
        if (targetVariable != null)
        {
            target = targetVariable.Value;
        }
    }
    public void SetRotation(GameObjectVariable value)
    {
        SetRotation(value.Value);
    }

    public void SetRotation(GameObject value)
    {
        Vector3 relativePos = value.transform.position - objToRotate.transform.position;
        Vector3 rotation = Quaternion.LookRotation(relativePos, Vector3.up).eulerAngles;
        if ((axis & Axis.x) == 0)
        {
            rotation.x = objToRotate.transform.rotation.x;
        }
        if ((axis & Axis.y) == 0)
        {
            rotation.y = objToRotate.transform.rotation.y;
        }
        if ((axis & Axis.z) == 0)
        {
            rotation.z = objToRotate.transform.rotation.z;
        }
        objToRotate.transform.localEulerAngles = rotation;
    }

    private void Update()
    {
        if (target != null)
        {
            SetRotation(target);
        }
    }
}
