using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerLookDirection : MonoBehaviour, IInputDirection
{
    public Vector2 direction
    {
        get
        {
            Vector2 gamepadLook = Vector2.zero;
            if (Gamepad.current != null)
            {
                StickControl stick = Gamepad.current.rightStick;
                gamepadLook = new Vector2(stick.up.value - stick.down.value, stick.right.value - stick.left.value) * 12f;
            }
            Vector2 mouseLook = new Vector2(Mouse.current.delta.value.y, Mouse.current.delta.value.x);
            return mouseLook + gamepadLook;
        }
    }
}
