using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PlayerAttackActionInput : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonWest.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.Mouse1) || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonWest.isPressed;
            }
            return Input.GetKey(KeyCode.Mouse1) || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonWest.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.Mouse1) || gamepad;
        }
    }
}
