using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PlayerAnyButtonTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonWest.wasPressedThisFrame ||
                    Gamepad.current.buttonEast.wasPressedThisFrame ||
                    Gamepad.current.leftShoulder.wasPressedThisFrame ||
                    Gamepad.current.buttonNorth.wasPressedThisFrame ||
                    Gamepad.current.buttonSouth.wasPressedThisFrame ||
                    Gamepad.current.rightTrigger.wasPressedThisFrame;
            }
            bool keyboard = Input.GetKeyDown(KeyCode.Mouse1) ||
                Input.GetKeyDown(KeyCode.LeftShift) ||
                Input.GetKeyDown(KeyCode.G) ||
                Input.GetKeyDown(KeyCode.Mouse2) ||
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.Mouse0);
            return keyboard || gamepad;
        }
    }

    public bool buttonHold
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonWest.isPressed ||
                    Gamepad.current.buttonEast.isPressed ||
                    Gamepad.current.leftShoulder.isPressed ||
                    Gamepad.current.buttonNorth.isPressed ||
                    Gamepad.current.buttonSouth.isPressed ||
                    Gamepad.current.rightTrigger.isPressed;
            }
            bool keyboard = Input.GetKey(KeyCode.Mouse1) ||
                Input.GetKey(KeyCode.LeftShift) ||
                Input.GetKey(KeyCode.G) ||
                Input.GetKey(KeyCode.Mouse2) ||
                Input.GetKey(KeyCode.Space) ||
                Input.GetKey(KeyCode.Mouse0);
            return keyboard || gamepad;
        }
    }

    public bool buttonUp
    {
        get
        {
            bool gamepad = false;
            if (Gamepad.current != null)
            {
                gamepad = Gamepad.current.buttonWest.wasReleasedThisFrame ||
                    Gamepad.current.buttonEast.wasReleasedThisFrame ||
                    Gamepad.current.leftShoulder.wasReleasedThisFrame ||
                    Gamepad.current.buttonNorth.wasReleasedThisFrame ||
                    Gamepad.current.buttonSouth.wasReleasedThisFrame ||
                    Gamepad.current.rightTrigger.wasReleasedThisFrame;
            }
            bool keyboard = Input.GetKeyUp(KeyCode.Mouse1) ||
                Input.GetKeyUp(KeyCode.LeftShift) ||
                Input.GetKeyUp(KeyCode.G) ||
                Input.GetKeyUp(KeyCode.Mouse2) ||
                Input.GetKeyUp(KeyCode.Space) ||
                Input.GetKeyUp(KeyCode.Mouse0);
            return keyboard || gamepad;
        }
    }
}
