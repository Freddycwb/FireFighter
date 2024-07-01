using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System;

public class PlaneMove : MonoBehaviour
{
    [SerializeField] private GameObject input;
    private IInputDirection _input;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float maxSpeed;
    private float _startMaxSpeed;
    [SerializeField] private float maxAccel;
    private float _startMaxAccel;
    [SerializeField] private float maxDesAccel;
    private float _startMaxDesAccel;
    private bool _moving;

    private Vector3 _lastDir = new Vector3(0, 0, 0);

    [SerializeField] private bool canControl = true;

    public Action onStartMove;
    public Action onStopMove;

    public Vector3 GetLastDir()
    {
        return _lastDir;
    }

    public void SetCanControl(bool value)
    {
        canControl = value;
    }

    private void Start()
    {
        _input = input.GetComponent<IInputDirection>();
        _startMaxSpeed = maxSpeed;
        _startMaxAccel = maxAccel;
        _startMaxDesAccel = maxDesAccel;
    }

    public void SetMaxSpeed(float value)
    {
        maxSpeed = value;
    }

    public void SetMaxAccel(float value)
    {
        maxAccel = value;
    }

    public void SetMaxDesAccel(float value)
    {
        maxDesAccel = value;
    }

    public void ResetMaxSpeed()
    {
        maxSpeed = _startMaxSpeed;
        maxAccel = _startMaxAccel;
        maxDesAccel = _startMaxDesAccel;
    }

    private void FixedUpdate()
    {
        Vector3 dir = Vector3.zero;
        if (_input != null && canControl)
        {
            dir = new Vector3(_input.direction.normalized.x, 0, _input.direction.normalized.y);
        }
        Move(dir);
        if (dir.magnitude > 0 && !_moving)
        {
            if (onStartMove != null)
            {
                onStartMove.Invoke();
            }
            _moving = true;
        }
        else if (dir.magnitude <= 0 && _moving)
        {
            if (onStopMove != null)
            {
                onStopMove.Invoke();
            }
            _moving = false;
        }
    }

    public void Move(Vector3 dir)
    {
        Vector3 goalVel = dir * maxSpeed;
        Vector3 neededAccel = goalVel - rb.velocity;
        neededAccel -= Vector3.up * neededAccel.y;
        neededAccel = dir != Vector3.zero ? Vector3.ClampMagnitude(neededAccel, maxAccel) : Vector3.ClampMagnitude(neededAccel, maxDesAccel);
        rb.AddForce(neededAccel, ForceMode.Impulse);
        if (dir.magnitude > 0)
        {
            _lastDir = dir.normalized;
        }
    }
}
