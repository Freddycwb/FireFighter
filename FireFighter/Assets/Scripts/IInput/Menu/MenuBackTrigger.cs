using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class MenuBackTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonEast.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonEast.isPressed || Gamepad.current.startButton.isPressed;
            }
            return Input.GetKey(KeyCode.Escape);
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonEast.wasReleasedThisFrame || Gamepad.current.startButton.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.Escape);
        }
    }
}
