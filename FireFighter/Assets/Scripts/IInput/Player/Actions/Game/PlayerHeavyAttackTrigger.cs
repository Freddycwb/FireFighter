using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PlayerHeavyAttackTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonNorth.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.Mouse2) || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonNorth.isPressed;
            }
            return Input.GetKey(KeyCode.Mouse2) || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonNorth.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.Mouse2) || gamepad;
        }
    }
}
