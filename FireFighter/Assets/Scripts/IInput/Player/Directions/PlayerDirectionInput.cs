using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerDirectionInput : MonoBehaviour, IInputDirection
{

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

            return move;
        }
    }
}
