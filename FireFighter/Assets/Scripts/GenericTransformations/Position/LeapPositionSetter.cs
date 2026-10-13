using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapPositionSetter : MonoBehaviour
{
    [SerializeField] private GameObject objToMove;
    [SerializeField] private float leapTime = 0.25f;
    [SerializeField] private float leapMaxHeight = 1;
    [SerializeField] private AnimationCurve leapCurve;
    private Vector3 velocity = Vector3.zero;

    private Coroutine _coroutine;

    private Vector3 _startPos;
    private Vector3 _endPos;

    public Action onStartLeap;
    public Action onEndLeap;

    private void Start()
    {
        if (objToMove == null)
        {
            objToMove = gameObject;
        }
    }

    public void SetSmoothTime(float value)
    {
        leapTime = value;
    }

    public void CancelLeap()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    public void Leap(Vector3 value)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _startPos = objToMove.transform.position;
        _endPos = value;
        if (onStartLeap != null)
        {
            onStartLeap.Invoke();
        }
        _coroutine = StartCoroutine(LeapRoutine());
    }

    public void Leap(GameObject value)
    {
        Leap(value.transform.position);
    }

    private IEnumerator LeapRoutine()
    {
        for (float i = 0; i < leapTime; i += Time.deltaTime)
        {
            float curveTime = i / leapTime;
            Vector3 newpos = Vector3.Lerp(_startPos, _endPos, curveTime);
            objToMove.transform.position = new Vector3(newpos.x, newpos.y + (leapCurve.Evaluate(curveTime) * leapMaxHeight), newpos.z);
            yield return new WaitForEndOfFrame();
        }
        objToMove.transform.position = _endPos;
        if (onEndLeap != null)
        {
            onEndLeap.Invoke();
        }
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}
