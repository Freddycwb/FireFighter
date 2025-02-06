using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    [SerializeField] private Rigidbody throwable;
    [SerializeField] private Transform target;

    [FormerlySerializedAs("throwableOffSet")] [SerializeField] private Vector3 targetOffset;
    [FormerlySerializedAs("throwableMaxOffSet")] [SerializeField] private Vector3 targetMaxOffSet;
    [SerializeField] private bool addForce;

    [SerializeField] private float valueAdjuster;
    [SerializeField] private OperatorType.Type valueAdjustType;

    public void SetTarget(GameObject value)
    {
        target = value.transform;
    }

    public void Throw()
    {
        Throw(throwable, target.position, addForce);
    }

    public void Throw(bool value)
    {
        Throw(throwable, target.position, value);
    }

    public void Throw(GameObject value)
    {
        Rigidbody rb = null;
        Transform t = null;
        if (value.GetComponent<Rigidbody>() == null)
        {
            rb = throwable;
            t = value.transform;
        }
        else
        {
            rb = value.GetComponent<Rigidbody>();
            t = target;
        }

        if (rb == null || t == null)
        {
            return;
        }

        Throw(rb, t.position, addForce);
    }

    public void Throw(Rigidbody rb, Vector3 target, bool addForce)
    {
        if (!addForce)
        {
            rb.velocity = Vector3.zero;
        }

        Vector3 offset = targetMaxOffSet != Vector3.zero ? new Vector3(Random.Range(targetOffset.x, targetMaxOffSet.x), Random.Range(targetOffset.y, targetMaxOffSet.y), Random.Range(targetOffset.z, targetMaxOffSet.z)) : targetOffset;

        Vector3 dirToObject = target + targetOffset - rb.transform.position;
        float throwForce = force.x >= force.y ? force.x : Random.Range(force.x, force.y);

        switch (valueAdjustType)
        {
            case OperatorType.Type.add:
                throwForce += valueAdjuster;
                break;
            case OperatorType.Type.subtract:
                throwForce -= valueAdjuster;
                break;
            case OperatorType.Type.divide:
                throwForce /= valueAdjuster;
                break;
            case OperatorType.Type.multiply:
                throwForce *= valueAdjuster;
                break;
            default:
                break;
        }

        rb.AddForce(dirToObject.normalized * throwForce, ForceMode.Impulse);
    }

    public void SetValueAdjuster(DamageChecker value)
    {
        valueAdjuster = value.GetLastDamage();
    }
}
