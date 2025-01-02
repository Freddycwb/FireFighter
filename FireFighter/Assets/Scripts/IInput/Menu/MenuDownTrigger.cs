using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class MenuDownTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.dpad.down.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.dpad.down.isPressed;
            }
            return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.dpad.down.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) || gamepad;
        }
    }
}
