using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Rendering.DebugUI;
using Unity.VisualScripting;

public class Gravity : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float gravityScale = -14;
    [SerializeField] private float maxSpeedUp = float.PositiveInfinity;
    [SerializeField] private float maxSpeedDown = float.NegativeInfinity;


    private bool _isGrounded = true;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Hover hover;

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
        Vector3 upVelocity = Vector3.up * Vector3.Dot(Vector3.up, rb.linearVelocity);
        if (upVelocity.y >= maxSpeedUp)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, maxSpeedUp, rb.linearVelocity.z);
        }

        Vector3 downVelocity = Vector3.down * Vector3.Dot(Vector3.down, rb.linearVelocity);
        if (downVelocity.y <= maxSpeedDown)
        {
            return;
        }

        Vector3 gravity = gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
        if (downVelocity.y <= maxSpeedDown)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -maxSpeedDown, rb.linearVelocity.z);
        }
    }

    private void CheckGround()
    {
        bool callLand;
        bool callTakeOff;

        if (hover == null)
        {
            Collider[] grounds = Physics.OverlapSphere(transform.position, groundCheckRadius, whatIsGround);

            callLand = !_isGrounded && grounds.Length > 0;
            callTakeOff = _isGrounded && grounds.Length <= 0;

            SetIsGrounded(grounds.Length > 0);
        }
        else
        {
            callLand = !_isGrounded && hover.GetGrounded();
            callTakeOff = _isGrounded && !hover.GetGrounded();

            SetIsGrounded(hover.GetGrounded());
        }



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
        Vector3 upVelocity = Vector3.down * Vector3.Dot(Vector3.up, rb.linearVelocity);
        if (upVelocity.y >= maxSpeedUp)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, maxSpeedUp, rb.linearVelocity.z);
        }
    }

    public void SetMaxSpeedDown(float value)
    {
        maxSpeedDown = value;
        Vector3 downVelocity = Vector3.down * Vector3.Dot(Vector3.down, rb.linearVelocity);
        if (downVelocity.y <= maxSpeedDown)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, maxSpeedDown, rb.linearVelocity.z);
        }
    }
}
