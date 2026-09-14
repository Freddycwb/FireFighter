using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ForwardDirection : MonoBehaviour, IInputDirection
{
    [SerializeField] private GameObject pov;

    public Vector2 direction
    {
        get
        {
            Vector2 move = Vector2.up;
            if (pov != null)
            {
                float headAngle = Mathf.Deg2Rad * (360 - pov.transform.rotation.eulerAngles.y);

                Vector2 a = new Vector2(Mathf.Cos(headAngle), Mathf.Sin(headAngle));
                Vector2 b = new Vector2(-Mathf.Sin(headAngle), Mathf.Cos(headAngle));

                move = move.x * a + move.y * b;
            }
            return move;
        }
    }
}
