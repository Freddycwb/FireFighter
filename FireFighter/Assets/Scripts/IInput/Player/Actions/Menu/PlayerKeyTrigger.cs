using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PlayerKeyActionTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonEast.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.U);
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonEast.isPressed;
            }
            return Input.GetKey(KeyCode.U);
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonEast.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.U);
        }
    }
}
