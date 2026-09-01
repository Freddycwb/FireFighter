using UnityEngine;
using UnityEngine.Events;

public class AudioLocalizeEvents : MonoBehaviour
{
    [SerializeField] private AudioLocalize audioLocalize;

    [SerializeField] private UnityEvent onAudioStart;
    [SerializeField] private UnityEvent onAudioEnd;

    private bool listening;

    private void OnEnable()
    {
        if (audioLocalize != null && !listening)
        {
            audioLocalize.onAudioStart += OnAudioStart;
            audioLocalize.onAudioEnd += OnAudioEnd;
            listening = true;
        }
    }

    void OnAudioStart()
    {
        if (enabled)
        {
            onAudioStart.Invoke();
        }
    }

    void OnAudioEnd()
    {
        if (enabled)
        {
            onAudioEnd.Invoke();
        }
    }

    private void OnDisable()
    {
        if (audioLocalize != null && listening)
        {
            audioLocalize.onAudioStart -= OnAudioStart;
            audioLocalize.onAudioEnd -= OnAudioEnd;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
