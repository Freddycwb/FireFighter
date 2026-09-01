using UnityEngine;
using UnityEngine.Events;

public class TextGradualEvents : MonoBehaviour
{
    [SerializeField] private TextGradual textGradual;

    [SerializeField] private UnityEvent onStartTyping;
    [SerializeField] private UnityEvent onSkipTyping;
    [SerializeField] private UnityEvent onStopTyping;

    private bool listening;

    private void OnEnable()
    {
        if (textGradual != null && !listening)
        {
            textGradual.onStartTyping += OnStartTyping;
            textGradual.onSkipTyping += OnSkipTyping;
            textGradual.onStopTyping += OnStopTyping;
            listening = true;
        }
    }

    void OnStartTyping()
    {
        if (enabled)
        {
            onStartTyping.Invoke();
        }
    }

    void OnSkipTyping()
    {
        if (enabled)
        {
            onSkipTyping.Invoke();
        }
    }

    void OnStopTyping()
    {
        if (enabled)
        {
            onStopTyping.Invoke();
        }
    }

    private void OnDisable()
    {
        if (textGradual != null && listening)
        {
            textGradual.onStartTyping -= OnStartTyping;
            textGradual.onSkipTyping -= OnSkipTyping;
            textGradual.onStopTyping -= OnStopTyping;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
