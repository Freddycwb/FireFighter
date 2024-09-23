using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PlayerJumpActionInput : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonSouth.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.Space) || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonSouth.isPressed;
            }
            return Input.GetKey(KeyCode.Space) || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonSouth.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.Space) || gamepad;
        }
    }
}
