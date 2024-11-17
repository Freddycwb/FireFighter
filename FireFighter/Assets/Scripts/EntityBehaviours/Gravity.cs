using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Rendering.DebugUI;

public class Gravity : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float gravityScale = -14;
    [SerializeField] private float maxSpeedUp = float.PositiveInfinity;
    [SerializeField] private float maxSpeedDown = float.NegativeInfinity;


    private bool _isGrounded = true;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask whatIsGround;

    public Action onLand;
    public Action onTakeOff;

    public bool GetIsGrounded()
    {
        return _isGrounded;
    }

    public void SetIsGrounded(bool value)
    {
        _isGrounded = value;
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
        Vector3 upVelocity = Vector3.up * Vector3.Dot(Vector3.up, rb.velocity);
        if (upVelocity.y >= maxSpeedUp)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeedUp, rb.velocity.z);
        }

        Vector3 downVelocity = Vector3.down * Vector3.Dot(Vector3.down, rb.velocity);
        if (downVelocity.y <= maxSpeedDown)
        {
            return;
        }

        Vector3 gravity = gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
        if (downVelocity.y <= maxSpeedDown)
        {
            rb.velocity = new Vector3(rb.velocity.x, -maxSpeedDown, rb.velocity.z);
        }
    }

    private void CheckGround()
    {
        Collider[] grounds = Physics.OverlapSphere(transform.position, groundCheckRadius, whatIsGround);

        bool callLand = !_isGrounded && grounds.Length > 0;
        bool callTakeOff = _isGrounded && grounds.Length <= 0;

        SetIsGrounded(grounds.Length > 0);

        if (callLand)
        {
            if (onLand != null)
            {
                onLand.Invoke();
            }
        }
        else if (callTakeOff)
        {
            if (onTakeOff != null)
            {
                onTakeOff.Invoke();
            }
        }
    }

    public void SetGravityScale(float value)
    {
        gravityScale = value;
    }

    public void SetMaxSpeedUp(float value)
    {
        maxSpeedUp = value;
        Vector3 upVelocity = Vector3.down * Vector3.Dot(Vector3.up, rb.velocity);
        if (upVelocity.y >= maxSpeedUp)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeedUp, rb.velocity.z);
        }
    }

    public void SetMaxSpeedDown(float value)
    {
        maxSpeedDown = value;
        Vector3 downVelocity = Vector3.down * Vector3.Dot(Vector3.down, rb.velocity);
        if (downVelocity.y <= maxSpeedDown)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeedDown, rb.velocity.z);
        }
    }
}
