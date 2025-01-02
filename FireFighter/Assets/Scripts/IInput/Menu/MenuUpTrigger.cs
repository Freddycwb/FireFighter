using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class MenuUpTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.dpad.up.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.dpad.up.isPressed;
            }
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.dpad.up.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || gamepad;
        }
    }
}
