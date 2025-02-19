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

    private static bool _isPaused;
    private static bool _lateIsPaused;

    private void Start()
    {
        defaultFixedDeltaTime = Time.fixedDeltaTime;
        defaultVFXFixedTimeStep = VFXManager.fixedTimeStep;
        _isPaused = false;
    }

    public static bool GetIsPaused()
    {
        return _isPaused;
    }

    public static bool GetJustPaused()
    {
        bool result = _isPaused && !_lateIsPaused;
        _lateIsPaused = _isPaused;
        return result;
    }

    public static bool GetJustUnpaused()
    {
        bool result = !_isPaused && _lateIsPaused;
        return result;
    }

    public void SetTimeScale(float value)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        timeScale = value;
        Time.timeScale = 1 * timeScale;
        Time.fixedDeltaTime = defaultFixedDeltaTime * timeScale;
        VFXManager.fixedTimeStep = defaultVFXFixedTimeStep * timeScale;
    }

    public void SetTimeScaleSmooth(float value)
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
            Time.fixedDeltaTime = defaultFixedDeltaTime * Mathf.Clamp(freezeFrameCurve.Evaluate(count), 0, 1) * timeScale;
            VFXManager.fixedTimeStep = defaultVFXFixedTimeStep * Mathf.Clamp(freezeFrameCurve.Evaluate(count), 0, 1) * timeScale;
            count += Time.unscaledDeltaTime / freezeFrameDuration;
            if (count >= 1)
            {
                count = 1;     
            }
        }
        else
        {
            Time.timeScale = 1 * timeScale;
            Time.fixedDeltaTime = defaultFixedDeltaTime * timeScale;
            VFXManager.fixedTimeStep = defaultVFXFixedTimeStep * timeScale;
        }
    }

    private void LateUpdate()
    {
        _lateIsPaused = _isPaused;
        if (Time.timeScale == 0)
        {
            _isPaused = true;
        }
        else
        {
            _isPaused = false;
        }
    }
}
