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

    [SerializeField] private bool setIsGroundedOnUpdate = true;
    private bool _isGrounded = true;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask whatIsGround;

    private bool _isFalling = true;

    [SerializeField] private Hover hover;
    [SerializeField] private List<GameObject> collidersToIgnore = new List<GameObject>();

    public Action onLand;
    public Action onTakeOff;

    public Action onStartFalling;
    public Action onStartRising;

    public bool GetIsGrounded()
    {
        return _isGrounded;
    }

    public void SetIsGrounded(bool value)
    {
        _isGrounded = value;
    }

    public void SetIsGroundedOnUpdate(bool value)
    {
        setIsGroundedOnUpdate = value;
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
        if (setIsGroundedOnUpdate)
        {
            CheckGround();
        }
        CheckIsFalling();
    }

    private void ApplyGravity()
    {
        Vector3 gravity = gravityScale * Vector3.up;

        if (!_isGrounded || !setIsGroundedOnUpdate) {
                rb.AddForce(gravity, ForceMode.Acceleration);
        }

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, maxSpeedDown, maxSpeedUp), rb.linearVelocity.z);
    }

    private void CheckGround()
    {
        bool callLand;
        bool callTakeOff;

        if (hover == null)
        {
            int grounds = 0;
            Collider[] colliders = Physics.OverlapSphere(transform.position, groundCheckRadius, whatIsGround);
            grounds = colliders.Length;

            foreach(Collider col in colliders)
            {
                foreach (GameObject gameObj in collidersToIgnore)
                {
                    if (col.gameObject == gameObj)
                    {
                        grounds--;
                    }
                }
            }

            callLand = !_isGrounded && grounds > 0;
            callTakeOff = _isGrounded && grounds <= 0;

            SetIsGrounded(grounds > 0);
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

    private void CheckIsFalling()
    {
        if (rb.linearVelocity.y <= 0 && !_isFalling)
        {
            _isFalling = true;
            if (onStartFalling != null)
            {
                onStartFalling.Invoke();
            }
        }
        else if (rb.linearVelocity.y > 0 && _isFalling)
        {
            _isFalling = false;
            if (onStartRising != null)
            {
                onStartRising.Invoke();
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

    public void SetFallSpeed(float value)
    {
        Vector3 downVelocity = Vector3.down * Vector3.Dot(Vector3.down, rb.linearVelocity);
        if (downVelocity.y <= value)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, value, rb.linearVelocity.z);
        }
    }
}
