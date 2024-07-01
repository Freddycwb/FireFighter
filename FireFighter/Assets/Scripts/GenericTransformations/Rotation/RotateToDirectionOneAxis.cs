using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class RotateToDirectionOneAxis : MonoBehaviour
{
    [SerializeField] private GameObject input;
    private IInputDirection _input;

    [SerializeField] private float rotateVel;
    [SerializeField] private float offset;

    public void SetInput(GameObject value)
    {
        input = value;
    }

    private void Start()
    {
        if (input != null && input.GetComponent<IInputDirection>() != null)
        {
            _input = input.GetComponent<IInputDirection>();
        }
    }

    private void FixedUpdate()
    {
        if (_input == null)
        {
            return;
        }
        Rotate();
    }

    private void Rotate()
    {
        if (_input.direction.magnitude != 0)
        {
            Vector2 dir = _input.direction.normalized;
            Quaternion target = Quaternion.identity;
            float rotY = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            target = Quaternion.Euler(transform.localEulerAngles.x, -rotY + offset, transform.localEulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotateVel);
        }
    }
}
