using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PositionShake : MonoBehaviour
{
    [SerializeField] private GameObject objToShake;
    [SerializeField] private Vector3 axisIntensity = new Vector3(1,1,1);
    [SerializeField] private float shakeIntensity = 1;
    [SerializeField] private AnimationCurve intensityCurve;
    [SerializeField] private bool backToOriginAtEnd;

    private Coroutine _coroutine;
    private float _time;
    private float _lastFullTime;
    private float _intensity;
    private float _lastFullIntensity;
    private float _delayBetweenShake;
    private Vector3 _position;

    private void OnEnable()
    {
        _position = objToShake.transform.localPosition;
    }

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

    private void Update()
    {
        if (_time > 0)
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
            RandomizeLookAtOffset();
            _intensity = intensityCurve.Evaluate(_time / _lastFullTime) * _lastFullIntensity;
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
}
