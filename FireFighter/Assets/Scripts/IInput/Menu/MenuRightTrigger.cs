using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class MenuRightTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.dpad.right.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.dpad.right.isPressed;
            }
            return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.dpad.right.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || gamepad;
        }
    }
}
