using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManagerEvents : MonoBehaviour
{
    [SerializeField] private SceneManager sceneManager;

    [SerializeField] private UnityEvent onStartLoadScene;
    [SerializeField] private UnityEvent onLastFrameBeforeLoadScene;

    private bool listening;

    private void OnEnable()
    {
        if (sceneManager != null)
        {
            sceneManager.onStartLoadScene += OnStartLoadScene;
            sceneManager.onLastFrameBeforeLoadScene += OnLastFrameBeforeLoadScene;
            listening = true;
        }
    }

    void OnStartLoadScene(string value)
    {
        if (enabled)
        {
            onStartLoadScene.Invoke();
        }
    }

    void OnLastFrameBeforeLoadScene()
    {
        if (enabled)
        {
            onLastFrameBeforeLoadScene.Invoke();
        }
    }

    private void OnDisable()
    {
        if (onStartLoadScene != null && listening)
        {
            sceneManager.onStartLoadScene -= OnStartLoadScene;
            sceneManager.onLastFrameBeforeLoadScene -= OnLastFrameBeforeLoadScene;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
