using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ZeroDirection : MonoBehaviour, IInputDirection
{
    public Vector2 direction
    {
        get
        {
            return Vector2.zero;
        }
    }
}
