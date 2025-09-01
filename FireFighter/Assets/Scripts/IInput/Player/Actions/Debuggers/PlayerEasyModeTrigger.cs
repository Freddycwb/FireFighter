using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class PlayerEasyModeTrigger : MonoBehaviour, IInputAction
{

    public bool buttonDown
    {
        get
        {
            return Input.GetKeyDown(KeyCode.O);
        }
    }

    public bool buttonHold
    {
        get
        {
            return Input.GetKey(KeyCode.O);
        }
    }

    public bool buttonUp
    {
        get
        {
            return Input.GetKeyUp(KeyCode.O);
        }
    }
}
