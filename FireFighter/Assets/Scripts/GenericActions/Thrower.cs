using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    [SerializeField] private Rigidbody throwable;
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 throwableOffSet;
    [SerializeField] private Vector3 throwableMaxOffSet;
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

        Vector3 offset = throwableMaxOffSet != Vector3.zero ? new Vector3(Random.Range(throwableOffSet.x, throwableMaxOffSet.x), Random.Range(throwableOffSet.y, throwableMaxOffSet.y), Random.Range(throwableOffSet.z, throwableMaxOffSet.z)) : throwableOffSet;

        Vector3 dirToObject = target - (rb.transform.position + throwableOffSet);
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
