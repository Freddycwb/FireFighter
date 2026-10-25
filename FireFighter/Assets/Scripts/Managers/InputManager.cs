using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.XInput;

public class InputManager : MonoBehaviour
{
    private InputDevice _currDevice = null;

    private void Start()
    {
        ++InputUser.listenForUnpairedDeviceActivity;
        InputUser.onUnpairedDeviceUsed += OnDeviceUsed;
    }

    public void OnDeviceUsed(InputControl control, InputEventPtr e)
    {
        InputDevice device = control.device;

        if (device == _currDevice) return;
        _currDevice = device;

        if (device is DualShockGamepad) {
            Localization.ChangeInput("_playstation");
        } else if (device is Gamepad) {
            Localization.ChangeInput("_xbox");
        } else {
            Localization.ChangeInput("_keyboard");
        }
    }

    private void OnDestroy()
    {
        InputUser.onUnpairedDeviceUsed -= OnDeviceUsed;
        --InputUser.listenForUnpairedDeviceActivity;
    }
}
