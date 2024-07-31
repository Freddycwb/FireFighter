using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerDirectionInput : MonoBehaviour, IInputDirection
{
    [SerializeField] private GameObject pov;
    [SerializeField] private GameObject povVariable;

    public Vector2 direction
    {
        get
        {
            Vector2 gamepadMove = Vector2.zero;
            if (Gamepad.current != null)
            {
                StickControl stick = Gamepad.current.leftStick;
                gamepadMove = new Vector2(stick.right.value - stick.left.value, stick.up.value - stick.down.value);
                if (gamepadMove.magnitude < 0.5f)
                {
                    gamepadMove = Vector2.zero;
                }
            }
            Vector2 keyboardMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 move = keyboardMove + gamepadMove;

            pov = povVariable ? povVariable : pov;

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
