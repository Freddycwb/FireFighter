using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PlayerHealthUpgradeTrigger : MonoBehaviour, IInputAction
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
            return Input.GetKeyDown(KeyCode.T);
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
            return Input.GetKey(KeyCode.T);
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
            return Input.GetKeyUp(KeyCode.T);
        }
    }
}
