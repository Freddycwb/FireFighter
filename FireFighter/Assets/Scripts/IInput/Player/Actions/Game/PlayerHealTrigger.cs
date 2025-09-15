using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PlayerHealTrigger : MonoBehaviour, IInputAction
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
            return Input.GetKeyDown(KeyCode.G) || gamepad;
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
            return Input.GetKey(KeyCode.G) || gamepad;
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
            return Input.GetKeyUp(KeyCode.G) || gamepad;
        }
    }
}
