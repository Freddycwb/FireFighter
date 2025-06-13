using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System;

public class Jump : EntityAction
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Gravity gravity;

    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float holdJumpTime = 0.2f;
    private float _holdJump;
    [SerializeField] private float jumpPressedRememberTime = 0.2f;
    private float _jumpPressedRemember;
    [SerializeField] private float groundedRememberTime = 0.2f;
    private float _groundedRemember;
    private bool _justJumped;

    public Action onJump;
    public Action onStopHolding;

    public override void Update()
    {
        base.Update();
        SetRemembers();
    }

    private void FixedUpdate()
    {
        PerformJump();
    }

    private void SetRemembers()
    {
        if (_groundedRemember > 0)
        {
            _groundedRemember -= Time.deltaTime;
        }
        if (gravity.GetIsGrounded() && !_justJumped)
        {
            _groundedRemember = groundedRememberTime;
        }
        if (_jumpPressedRemember > 0)
        {
            _jumpPressedRemember -= Time.deltaTime;
        }
        if (GetButtonDown())
        {
            _jumpPressedRemember = jumpPressedRememberTime;
        }
    }

    private void PerformJump()
    {
        if ((_justJumped && GetButtonHold()) || ((_jumpPressedRemember > 0) && (_groundedRemember > 0)))
        {
            if (!_justJumped)
            {
                _justJumped = true;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                _holdJump = holdJumpTime;
                _jumpPressedRemember = 0;
                _groundedRemember = 0;
                if (onJump != null)
                {
                    onJump.Invoke();
                }
            }
            else
            {
                if (_holdJump > 0)
                {
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                    _holdJump -= Time.deltaTime;
                    if (_holdJump <= 0)
                    {
                        if (onStopHolding != null)
                        {
                            onStopHolding.Invoke();
                        }
                    }
                }
            }
        }
        else
        {
            if (!GetButtonHold() && _justJumped)
            {
                if (onStopHolding != null)
                {
                    onStopHolding.Invoke();
                }
            }
            _justJumped = false;
        }
    }

    public void SetJumpForce(float value)
    {
        jumpForce = value;
    }
}
