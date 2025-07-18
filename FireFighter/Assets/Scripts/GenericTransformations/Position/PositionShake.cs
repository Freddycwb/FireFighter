using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.Rendering.DebugUI;

public class PositionShake : MonoBehaviour
{
    [SerializeField] private GameObject objToShake;
    [SerializeField] private Vector3 axisIntensity = new Vector3(1,1,1);
    [SerializeField] private float shakeIntensity = 1;
    [SerializeField] private AnimationCurve intensityCurve;
    [SerializeField] private bool backToOriginAtEnd;

    [System.Flags]
    public enum SourceTypes
    {
        None = 0,
        counter = 1,
        timer = 2,
    }
    [SerializeField] private SourceTypes sourceType = SourceTypes.timer;

    private Coroutine _coroutine;
    private float _time;
    private float _lastFullTime;
    private float _intensity;
    private float _lastFullIntensity;
    private float _delayBetweenShake;
    private Vector3 _position;

    private InvokeAfterCounter _counter;

    private void OnEnable()
    {
        _position = objToShake.transform.localPosition;
    }

    public void CallShakeByCounter(InvokeAfterCounter counter, Vector3 values)
    {
        sourceType = SourceTypes.counter;
        _counter = counter;
        CallShake(values);
    }

    public void CallShake(Vector3 value)
    {
        if ((sourceType & SourceTypes.counter) != 0)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _time = 1;
            _intensity = value.y;
            _lastFullIntensity = value.y;
            _delayBetweenShake = value.z;
            _coroutine = StartCoroutine(ShakeRoutine());
        }
        else if ((sourceType & SourceTypes.timer) != 0)
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
            _lastFullIntensity = value.y;
            _delayBetweenShake = value.z;
            _coroutine = StartCoroutine(ShakeRoutine());
        }
    }

    private void Update()
    {
        if (_time > 0 && !((sourceType & SourceTypes.counter) != 0))
        {
            _time -= Time.deltaTime;
            if (_time < 0)
            {
                _time = 0;
            }
        }
    }

    private IEnumerator ShakeRoutine()
    {
        while (_time > 0)
        {
            yield return new WaitForSeconds(_delayBetweenShake);
            if ((sourceType & SourceTypes.counter) != 0 && _counter != null)
            {
                _intensity = intensityCurve.Evaluate(_counter.GetCurrentValue() / (_counter.GetMaxValue() - _counter.GetMinValue())) * _lastFullIntensity;
            }
            else if ((sourceType & SourceTypes.timer) != 0)
            {
                _intensity = intensityCurve.Evaluate(_time / _lastFullTime) * _lastFullIntensity;
            }
            RandomizeLookAtOffset();
        }
        if (backToOriginAtEnd)
        {
            objToShake.transform.localPosition = _position;
        }
    }

    private void RandomizeLookAtOffset()
    {
        Vector3 newOffset = Vector3.zero;
        newOffset.x = Random.Range(-(shakeIntensity * axisIntensity.x), shakeIntensity * axisIntensity.x);
        newOffset.y = Random.Range(-(shakeIntensity * axisIntensity.y), shakeIntensity * axisIntensity.y);
        newOffset.z = Random.Range(-(shakeIntensity * axisIntensity.z), shakeIntensity * axisIntensity.z);
        objToShake.transform.localPosition = _position + (newOffset * _intensity);
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}
