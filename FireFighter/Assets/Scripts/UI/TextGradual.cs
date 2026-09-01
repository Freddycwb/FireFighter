using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TextGradual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private string displayText;
    private Coroutine _routine;
    [SerializeField] public float charDelay;
    [SerializeField] public float commaDelay;
    [SerializeField] public float brDelay;

    private char _lastChar;

    public Action onStartTyping;
    public Action onSkipTyping;
    public Action onStopTyping;

    public void CallType(string value)
    {
        tmp.text = "";
        if (_routine != null)
        {
            StopCoroutine(_routine);
        }
        _routine = StartCoroutine(TypeText(value));
    }

    public void CancelType()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
        }
    }

    IEnumerator TypeText(string value)
    {
        displayText = value;  
        tmp.text = "<line-height=100%>";
        if (onStartTyping != null)
        {
            onStartTyping.Invoke();
        }
        foreach (char c in value.ToCharArray())
        {
            tmp.text += c;
            if (!CheckBreakLine(c) && !(c == ' '))
            {
                yield return new WaitForSeconds(charDelay);
            }
            else if (_lastChar == '>')
            {
                yield return new WaitForSeconds(brDelay);
            }
            if (c == ',')
            {
                yield return new WaitForSeconds(commaDelay);
            }
        }
        _routine = null;
        if (onStopTyping != null)
        {
            onStopTyping.Invoke();
        }
    }

    public void SkipType()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
            tmp.text = "<line-height=100%>" + displayText;
            if (onSkipTyping != null)
            {
                onSkipTyping.Invoke();
            }
            if (onStopTyping != null)
            {
                onStopTyping.Invoke();
            }
        }
    }

    private bool CheckBreakLine(char value)
    {
        if (value == '<')
        {
            _lastChar = '<';
            return true;
        }
        else if(_lastChar == '<' && value == 'b')
        {
            _lastChar = 'b';
            return true;
        }
        else if (_lastChar == 'b' && value == 'r')
        {
            _lastChar = 'r';
            return true;
        }
        else if (_lastChar == 'r' && value == '>')
        {
            _lastChar = '>';
            return true;
        }
        else
        {
            _lastChar = 'a';
            return false;
        }
    }
}
