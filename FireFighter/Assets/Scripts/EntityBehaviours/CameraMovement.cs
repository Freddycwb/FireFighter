using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObjectVariable targetVariable;

    [SerializeField] private float maxDistance;
    [SerializeField] private float distanceMargin;
    [SerializeField] private float yOffset;
    private Vector3 _targetPos => target.transform.position + Vector3.up * yOffset;

    private Vector2 _orbitRotation;
    [SerializeField] private Vector2 angleLimits;

    [SerializeField] private GameObject lookDirectionObject;
    private IInputDirection _lookDirection;
    [SerializeField] private float sensitivity;

    [SerializeField] private LayerMask obstacleMask;

    private void OnEnable()
    {
        if (targetVariable != null)
        {
            target = targetVariable.Value;
        }

        _lookDirection = lookDirectionObject.GetComponent<IInputDirection>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!target) return;

        // Updating camera rotation
        Vector2 scaledLook = _lookDirection.direction * sensitivity;
        _orbitRotation = new Vector2(Mathf.Clamp(_orbitRotation.x - scaledLook.x, angleLimits.x, angleLimits.y), Mathf.Repeat(_orbitRotation.y - scaledLook.y, 360));

        // Calculating camera direction
        Vector3 orbitRotationRad = _orbitRotation * Mathf.Deg2Rad;
        float circleSize = Mathf.Abs(Mathf.Cos(orbitRotationRad.x));
        Vector3 direction = new Vector3(Mathf.Cos(orbitRotationRad.y), 0, Mathf.Sin(orbitRotationRad.y)) * circleSize + Vector3.up * Mathf.Sin(orbitRotationRad.x);

        // Checking for walls
        RaycastHit hit;
        Physics.Raycast(_targetPos, direction, out hit, maxDistance, obstacleMask);

        float hitDistance = (hit.collider ? hit.distance : maxDistance) - distanceMargin;

        // Setting camera position
        transform.position = _targetPos + direction * hitDistance;
    }
}
