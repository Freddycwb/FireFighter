using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System;

public class Jump : EntityAction
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Gravity gravity;

    [SerializeField] private float jumpForce;
    [SerializeField] private float holdJumpTime;
    private float _holdJump;
    [SerializeField] private float jumpPressedRememberTime;
    private float _jumpPressedRemember;
    [SerializeField] private float groundedRememberTime;
    private float _groundedRemember;
    private bool _justJumped;

    public Action onJump;
    public Action onStopHolding;

    private void Update()
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
        if ((GetButtonHold() && _justJumped) || ((_jumpPressedRemember > 0) && (_groundedRemember > 0)))
        {
            if (!_justJumped)
            {
                _justJumped = true;
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
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
                    rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                    _holdJump -= Time.deltaTime;
                    if (_holdJump <= 0)
                    {
                        onStopHolding.Invoke();
                    }
                }
            }
        }
        else
        {
            if (!GetButtonHold() && _justJumped)
            {
                onStopHolding.Invoke();
            }
            _justJumped = false;
        }
    }
}
