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

    [SerializeField] private Vector3 originOffset;
    [SerializeField] private Vector3 originMaxOffSet;
    [SerializeField] private bool originOffsetIsLocal;

    [FormerlySerializedAs("throwableOffSet")] [SerializeField] private Vector3 targetOffset;
    [FormerlySerializedAs("throwableMaxOffSet")] [SerializeField] private Vector3 targetMaxOffSet;

    [SerializeField] private bool addForce;
    [SerializeField] private bool useCenterOfMass;

    [SerializeField] private float valueAdjuster;
    [SerializeField] private OperatorType.Type valueAdjustType;

    public float GetForceX()
    {
        return force.x;
    }

    public float GetForceY()
    {
        return force.y;
    }

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

    private float GetThrowForce()
    {
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

        return throwForce;
    }

    public void Throw(Rigidbody rb, Vector3 target, bool addForce)
    {
        if (!enabled)
        {
            return;
        }
        if (!addForce)
        {
            rb.linearVelocity = Vector3.zero;
        }

        Vector3 newTargetOffset = targetMaxOffSet != Vector3.zero ? new Vector3(Random.Range(targetOffset.x, targetMaxOffSet.x), Random.Range(targetOffset.y, targetMaxOffSet.y), Random.Range(targetOffset.z, targetMaxOffSet.z)) : targetOffset;
        Vector3 newOriginOffset = originMaxOffSet != Vector3.zero ? new Vector3(Random.Range(originOffset.x, originMaxOffSet.x), Random.Range(originOffset.y, originMaxOffSet.y), Random.Range(originOffset.z, originMaxOffSet.z)) : originOffset;
        Vector3 dirToObject = (target + newTargetOffset) - ((!useCenterOfMass? rb.transform.position : rb.worldCenterOfMass) + (!originOffsetIsLocal? newOriginOffset : rb.transform.TransformDirection(newOriginOffset)));

        rb.AddForce(dirToObject.normalized * GetThrowForce(), ForceMode.Impulse);
    }

    public void ThrowUsingRBVelocity(Rigidbody value)
    {
        Throw(throwable, value, addForce);
    }

    public void Throw(Rigidbody rb, Rigidbody dir, bool addForce)
    {
        if (!enabled)
        {
            return;
        }
        if (!addForce)
        {
            rb.linearVelocity = Vector3.zero;
        }

        Vector3 offset = targetMaxOffSet != Vector3.zero ? new Vector3(Random.Range(targetOffset.x, targetMaxOffSet.x), Random.Range(targetOffset.y, targetMaxOffSet.y), Random.Range(targetOffset.z, targetMaxOffSet.z)) : targetOffset;

        rb.AddForce(dir.linearVelocity.normalized * GetThrowForce(), ForceMode.Impulse);
    }

    public void SetValueAdjuster(DamageChecker value)
    {
        valueAdjuster = value.GetLastDamage();
    }

    public void SetForce(DamageChecker value)
    {
        force = -value.GetLastKnockbackForce();
    }

    public void SetForceX(float value)
    {
        force.x = value;
    }

    public void SetForceX(AnimationCurveValue value)
    {
        SetForceX(value.GetValueByCounter());
    }

    public void SetForceY(float value)
    {
        force.y = value;
    }

    public void SetForceY(AnimationCurveValue value)
    {
        SetForceY(value.GetValueByCounter());
    }
}
