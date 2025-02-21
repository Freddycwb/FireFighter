using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private LookAt cameraLookAt;
    [SerializeField] private Vector3 axisIntensity;
    [SerializeField] private FloatVariable ScreenShakeIntensity;
    [SerializeField] private AnimationCurve intensityCurve;

    private Coroutine _coroutine;
    private float _time;
    private float _lastFullTime;
    private float _intensity;
    private float _lastFullIntensity;
    private float _delayBetweenShake;

    public void CallShake(Vector3 value)
    {
        if (_coroutine != null && _time < value.x)
        {
            StopCoroutine(_coroutine);
        }
        else if (_coroutine != null && _time >= value.x)
        {
            return;
        }
        _time = value.x;
        _lastFullTime = value.x;
        _intensity = value.y;
        _lastFullIntensity = value.y;
        _delayBetweenShake = value.z;
        _coroutine = StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        while (_time > 0)
        {
            yield return new WaitForSeconds(_delayBetweenShake);
            RandomizeLookAtOffset();
            _time -= _delayBetweenShake;
            _intensity = intensityCurve.Evaluate(_time / _lastFullTime) * _lastFullIntensity;
        }
    }

    private void RandomizeLookAtOffset()
    {
        float intensity = 1;
        if (ScreenShakeIntensity != null)
        {
            intensity = ScreenShakeIntensity.Value;
        }
        Vector3 newOffset = Vector3.zero;
        newOffset.x = Random.Range(-(_intensity * axisIntensity.x), _intensity * axisIntensity.x);
        newOffset.y = Random.Range(-(_intensity * axisIntensity.y), _intensity * axisIntensity.y);
        newOffset.z = Random.Range(-(_intensity * axisIntensity.z), _intensity * axisIntensity.z);
        cameraLookAt.SetOffset(newOffset * intensity);
    }
}
