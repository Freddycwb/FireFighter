using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class MenuAltRightTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.rightShoulder.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.E) || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.rightShoulder.isPressed;
            }
            return Input.GetKey(KeyCode.E) || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.rightShoulder.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.E) || gamepad;
        }
    }
}
