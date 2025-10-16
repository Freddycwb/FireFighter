using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private Vector2 force;
    [SerializeField] private Rigidbody spinnable;
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private Vector3 targetMaxOffSet;

    [SerializeField] private bool addForce;
    [SerializeField] private bool useCenterOfMass;

    [SerializeField] private float valueAdjuster;
    [SerializeField] private OperatorType.Type valueAdjustType;

    private void Start()
    {
        spinnable.maxAngularVelocity = Mathf.Max(Mathf.Abs(force.x), Mathf.Abs(force.y));

        switch (valueAdjustType)
        {
            case OperatorType.Type.add:
                spinnable.maxAngularVelocity += valueAdjuster;
                break;
            case OperatorType.Type.subtract:
                spinnable.maxAngularVelocity -= valueAdjuster;
                break;
            case OperatorType.Type.divide:
                spinnable.maxAngularVelocity /= valueAdjuster;
                break;
            case OperatorType.Type.multiply:
                spinnable.maxAngularVelocity *= valueAdjuster;
                break;
            default: break;
        }
    }

    public void SetTarget(GameObject value)
    {
        target = value.transform;
    }

    public void Spin()
    {
        Spin(spinnable, target.position, addForce);
    }

    public void Spin(bool value)
    {
        Spin(spinnable, target.position, value);
    }

    public void Spin(GameObject value)
    {
        Rigidbody rb = null;
        Transform t = null;
        if (value.GetComponent<Rigidbody>() == null)
        {
            rb = spinnable;
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

        Spin(rb, t.position, addForce);
    }

    public void Spin(Rigidbody rb, Vector3 target, bool addForce)
    {
        if (!addForce)
        {
            rb.linearVelocity = Vector3.zero;
        }

        Vector3 offset = targetMaxOffSet != Vector3.zero ? new Vector3(Random.Range(targetOffset.x, targetMaxOffSet.x), Random.Range(targetOffset.y, targetMaxOffSet.y), Random.Range(targetOffset.z, targetMaxOffSet.z)) : targetOffset;
        Vector3 dirToObject = target + offset - (!useCenterOfMass ? rb.transform.position : rb.transform.position + rb.centerOfMass);
        float spinForce = force.x >= force.y ? force.x : Random.Range(force.x, force.y);

        switch (valueAdjustType)
        {
            case OperatorType.Type.add:
                spinForce += valueAdjuster;
                break;
            case OperatorType.Type.subtract:
                spinForce -= valueAdjuster;
                break;
            case OperatorType.Type.divide:
                spinForce /= valueAdjuster;
                break;
            case OperatorType.Type.multiply:
                spinForce *= valueAdjuster;
                break;
            default:
                break;
        }

        rb.AddTorque(dirToObject * spinForce, ForceMode.Impulse);
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
}
