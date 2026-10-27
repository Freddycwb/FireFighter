using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class RumbleManager : MonoBehaviour
{
    private Gamepad pad;
    private Coroutine StopRumbleCoroutine;

    public void RumblePulse(Vector3 value)
    {
        pad = Gamepad.current;

        if (pad == null)
        {
            return;
        }

        if (StopRumbleCoroutine != null)
        {
            StopCoroutine(StopRumbleCoroutine);
        }

        pad.SetMotorSpeeds(value.x, value.y);
        StopRumbleCoroutine = StartCoroutine(RumbleDuration(value.z));
    }

    public void RumblePulse(GameEventVector3 value)
    {
        RumblePulse(value.value);
    }

    private IEnumerator RumbleDuration(float value)
    {
        yield return new WaitForSeconds(value);
        StopRumble();
    }

    public void StopRumble()
    {
        pad.SetMotorSpeeds(0f, 0f);
    }
}
