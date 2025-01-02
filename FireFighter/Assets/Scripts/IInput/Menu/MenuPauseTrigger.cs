using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class MenuPauseTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.startButton.wasPressedThisFrame;
            }
            return Input.GetKeyDown(KeyCode.Escape) || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.startButton.isPressed;
            }
            return Input.GetKey(KeyCode.Escape) || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.startButton.wasReleasedThisFrame;
            }
            return Input.GetKeyUp(KeyCode.Escape) || gamepad;
        }
    }
}
