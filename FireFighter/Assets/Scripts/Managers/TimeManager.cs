using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TimeManager : MonoBehaviour
{
    private float count;
    [SerializeField] private float timeScale;
    [SerializeField] private float freezeFrameDuration;
    [SerializeField] private AnimationCurve freezeFrameCurve;

    private Coroutine coroutine;
    [SerializeField] private float timeScaleSwitchValueSpeed;

    private float defaultFixedDeltaTime;
    private float defaultVFXFixedTimeStep;

    private void Start()
    {
        defaultFixedDeltaTime = Time.fixedDeltaTime;
        defaultVFXFixedTimeStep = VFXManager.fixedTimeStep;
    }

    public void SetTimeScale(float value)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(TimeScaleRoutine(value));
    }

    private IEnumerator TimeScaleRoutine(float value)
    {
        float startTimeScale = timeScale;
        while (Mathf.Abs(value - timeScale) > 0)
        {
            timeScale = value > timeScale ? timeScale + Time.unscaledDeltaTime * timeScaleSwitchValueSpeed : timeScale - Time.unscaledDeltaTime * timeScaleSwitchValueSpeed;
            if ((startTimeScale <= value && timeScale >= value) || (startTimeScale >= value && timeScale <= value))
            {
                timeScale = value;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void Update()
    {
        if (count < 1)
        {
            Time.timeScale = Mathf.Clamp(freezeFrameCurve.Evaluate(count), 0, 1) * timeScale;
            count += Time.unscaledDeltaTime / freezeFrameDuration;
            if (count >= 1)
            {
                count = 1;     
            }
        }
        else
        {
            Time.timeScale = 1 * timeScale;
        }
    }

    public void FreezeFrame(float value)
    {
        freezeFrameDuration = value;
        count = 0;
    }
}
