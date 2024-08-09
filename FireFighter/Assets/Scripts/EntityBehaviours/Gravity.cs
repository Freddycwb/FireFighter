using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gravity : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float gravityScale = -14;
    [SerializeField] private float maxSpeed = float.NegativeInfinity;


    private bool _isGrounded = true;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask whatIsGround;

    public Action onLand;
    public Action onTakeOff;

    public bool GetIsGrounded()
    {
        return _isGrounded;
    }

    private void Start()
    {
        if (rb == null && GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        CheckGround();
    }

    private void ApplyGravity()
    {
        Vector3 downVelocity = Vector3.down * Vector3.Dot(Vector3.down, rb.velocity);
        if (downVelocity.y <= maxSpeed)
        {
            return;
        }

        Vector3 gravity = gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
        if (downVelocity.y <= maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, -maxSpeed, rb.velocity.z);
        }
    }

    private void CheckGround()
    {
        Collider[] grounds = Physics.OverlapSphere(transform.position, groundCheckRadius, whatIsGround);
        if (!_isGrounded && grounds.Length > 0)
        {
            if (onLand != null)
            {
                onLand.Invoke();
            }
        }
        else if (_isGrounded && grounds.Length <= 0)
        {
            if (onTakeOff != null)
            {
                onTakeOff.Invoke();
            }
        }
        _isGrounded = grounds.Length > 0;
    }

    public void SetGravityScale(float value)
    {
        gravityScale = value;
    }

    public void SetMaxSpeed(float value)
    {
        maxSpeed = value;
        Vector3 downVelocity = Vector3.down * Vector3.Dot(Vector3.down, rb.velocity);
        if (downVelocity.y <= maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeed, rb.velocity.z);
        }
    }
}
