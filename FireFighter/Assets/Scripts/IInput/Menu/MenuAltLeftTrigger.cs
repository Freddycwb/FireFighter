using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class MenuAltLeftTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.leftShoulder.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.Q) || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.leftShoulder.isPressed;
            }
            return Input.GetKey(KeyCode.Q) || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.leftShoulder.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.Q) || gamepad;
        }
    }
}
