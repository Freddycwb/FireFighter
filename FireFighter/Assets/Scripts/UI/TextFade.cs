using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TextFade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private Coroutine _routine;
    [SerializeField] public float fadeSpeed;

    public void CallFadeIn()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
        }
        _routine = StartCoroutine(FadeIn());
    }

    public void CallFadeOut()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
        }
        _routine = StartCoroutine(FadeOut());
    }

    public void CancelType()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
        }
    }

    IEnumerator FadeIn()
    {
        while (tmp.color.a < 1)
        {
            tmp.color = tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, tmp.color.a + fadeSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeOut()
    {
        while (tmp.color.a > 0)
        {
            tmp.color = tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, tmp.color.a - fadeSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
