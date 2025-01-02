using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PlayerShootTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.rightTrigger.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.Mouse0) || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.rightTrigger.isPressed;
            }
            return Input.GetKey(KeyCode.Mouse0) || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.rightTrigger.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.Mouse0) || gamepad;
        }
    }
}
