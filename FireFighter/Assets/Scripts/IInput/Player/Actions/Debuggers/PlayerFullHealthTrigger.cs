using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PlayerFullHealthTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            return Input.GetKeyDown(KeyCode.I);
        }
    }

    public bool buttonHold
    {
        get
        {
            return Input.GetKey(KeyCode.I);
        }
    }

    public bool buttonUp
    {
        get
        {
            return Input.GetKeyUp(KeyCode.I);
        }
    }
}
