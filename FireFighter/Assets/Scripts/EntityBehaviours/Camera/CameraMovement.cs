using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObjectVariable targetVariable;

    [SerializeField] private float maxDistance;
    [SerializeField] private float distanceMargin;
    [SerializeField] private float distanceSmoothing;
    [SerializeField] private float yOffset;
    [SerializeField] private float lookYOffset;
    private Vector3 _targetPos => target.transform.position + Vector3.up * yOffset;
    private Vector3 _lookTargetPos => target.transform.position + Vector3.up * lookYOffset;

    private Vector2 _orbitRotation;
    [SerializeField] private Vector2 defaultRotation;
    [SerializeField] private Vector2 angleLimits;

    [SerializeField] private GameObject lookDirectionObject;
    private IInputDirection _lookDirection;
    [SerializeField] private bool canControl = true;
    [SerializeField] private FloatVariable sensitivity;
    [SerializeField] private BoolVariable invertX;
    [SerializeField] private BoolVariable invertY;

    [SerializeField] private LayerMask obstacleMask;

    private float _currDist;
    private float _currVel = 0.0f;

    private void OnEnable()
    {
        if (lookDirectionObject != null)
        {
            _lookDirection = lookDirectionObject.GetComponent<IInputDirection>();
        }

        _orbitRotation = defaultRotation;
    }

    private void Start()
    {
        _currDist = maxDistance;
        if (targetVariable != null)
        {
            target = targetVariable.Value;
        }
        HideCursor(true);
    }

    private void Update()
    {
        if (!target || TimeManager.GetIsPaused() || _lookDirection == null) return;

        // Updating camera rotation
        Vector2 scaledLook = canControl ? _lookDirection.direction * sensitivity.Value : Vector2.zero;
        if (invertY.Value)
        {
            scaledLook.x = -scaledLook.x;
        }
        if (invertX.Value)
        {
            scaledLook.y = -scaledLook.y;
        }
        _orbitRotation = new Vector2(Mathf.Clamp(_orbitRotation.x - scaledLook.x, angleLimits.x, angleLimits.y), Mathf.Repeat(_orbitRotation.y - scaledLook.y, 360));

        // Calculating camera direction
        Vector3 orbitRotationRad = _orbitRotation * Mathf.Deg2Rad;
        float circleSize = Mathf.Abs(Mathf.Cos(orbitRotationRad.x));
        Vector3 direction = new Vector3(Mathf.Cos(orbitRotationRad.y), 0, Mathf.Sin(orbitRotationRad.y)) * circleSize + Vector3.up * Mathf.Sin(orbitRotationRad.x);

        // Checking for walls
          // - updating direction to be based off lookTarget instead of target, so that camera doesn't rotate as it tries to move out of the wall
        Vector3 camPosFromLook = direction * maxDistance + Vector3.up * (yOffset - lookYOffset);
        direction = camPosFromLook.normalized;
          // ---

        RaycastHit hit;
        Physics.Raycast(_lookTargetPos, direction, out hit, camPosFromLook.magnitude + distanceMargin, obstacleMask);

        // Setting camera position
        float targetDist = camPosFromLook.magnitude;
        if (hit.collider) {
            // naive reduction of magnitude by the margin instead of moving along collision normal otherwise camera teleports when switching colliding surface
            // (there's definitely a better way, but it isn't worth it...)
            targetDist = hit.distance - distanceMargin;
        }

        transform.position = _lookTargetPos + direction * (_currDist = targetDist > _currDist? Mathf.SmoothDamp(_currDist, targetDist, ref _currVel, distanceSmoothing) : targetDist);
    }

    public void HideCursor(bool value)
    {
        if (value)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetInput(GameObject value)
    {
        _lookDirection = value.GetComponent<IInputDirection>();
    }

    public void SetCanControl(bool value)
    {
        canControl = value;
    }
}
