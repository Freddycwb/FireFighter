using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class RotateToDirectionOneAxis : MonoBehaviour
{
    [SerializeField] private GameObject objToRotate;
    [SerializeField] private GameObject input;
    private IInputDirection _input;

    public enum moveTypes
    {
        slerp = 0,
        linear = 1
    }

    [SerializeField] private moveTypes moveType = moveTypes.slerp;

    [SerializeField] private float rotateVel;
    [SerializeField] private float offset;

    [SerializeField] private float angleTriggerMove = 5;
    private Quaternion target;
    private bool moving;

    [SerializeField] private bool canControl = true;

    public Action onStartMove;
    public Action onStopMove;

    private void OnEnable()
    {
        if (objToRotate == null)
        {
            objToRotate = gameObject;
        }
    }

    public void SetInput(GameObject value)
    {
        input = value;
        _input = input.GetComponent<IInputDirection>();
    }

    public void SetMoveType(int value)
    {
        moveType = (moveTypes)value;
    }

    public void SetVelocity(FloatVariable value)
    {
        SetVelocity(value.Value);
    }

    public void SetVelocity(float value)
    {
        rotateVel = value;
    }

    private void Start()
    {
        if (input != null && input.GetComponent<IInputDirection>() != null)
        {
            _input = input.GetComponent<IInputDirection>();
        }
    }

    private void CheckMovement()
    {
        if (_input.direction.magnitude != 0)
        {
            if (Quaternion.Angle(objToRotate.transform.rotation, target) <= angleTriggerMove)
            {
                if (moving)
                {
                    if (onStopMove != null)
                    {
                        onStopMove.Invoke();
                    }
                    moving = false;
                }
            }
            else
            {
                if (!moving)
                {
                    if (onStartMove != null)
                    {
                        onStartMove.Invoke();
                    }
                    moving = true;
                }
            }
        }
        else
        {
            if (moving)
            {
                if (onStopMove != null)
                {
                    onStopMove.Invoke();
                }
                moving = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_input == null || !canControl)
        {
            return;
        }
        if (_input.direction.magnitude != 0)
        {
            switch (moveType)
            {
                case moveTypes.slerp:
                    RotateSlerp();
                    break;
                case moveTypes.linear:
                    RotateLinear();
                    break;
            }
        }
        CheckMovement();
    }

    private void RotateSlerp()
    {
        Vector2 dir = _input.direction.normalized;
        target = Quaternion.identity;
        float rotY = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        target = Quaternion.Euler(transform.localEulerAngles.x, -rotY + offset, transform.localEulerAngles.z);
        objToRotate.transform.rotation = Quaternion.Slerp(objToRotate.transform.rotation, target, Time.deltaTime * rotateVel);
    }

    private void RotateLinear()
    {
        Vector2 dir = _input.direction.normalized;
        target = Quaternion.identity;
        float rotY = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        target = Quaternion.Euler(transform.localEulerAngles.x, -rotY + offset, transform.localEulerAngles.z);
        objToRotate.transform.rotation = Quaternion.RotateTowards(objToRotate.transform.rotation, target, Time.deltaTime * rotateVel);
    }

    public void InstanteRotate(GameObject value)
    {
        if (_input.direction.normalized == Vector2.zero)
        {
            return;
        }
        Vector2 dir = _input.direction.normalized;
        target = Quaternion.identity;
        float rotY = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        target = Quaternion.Euler(transform.localEulerAngles.x, -rotY + offset, transform.localEulerAngles.z);
        value.transform.rotation = target;
    }

    public void SetCanControl(bool value)
    {
        canControl = value;
    }
}
