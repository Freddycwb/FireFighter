using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManagerEvents : MonoBehaviour
{
    [SerializeField] private SceneManager sceneManager;

    [SerializeField] private UnityEvent onStartLoadScene;
    [SerializeField] private UnityEvent onLastFrameBeforeLoadScene;
    [SerializeField] private UnityEvent<float> onLoadProgressChange;
    [SerializeField] private UnityEvent onFirstScene;

    private bool listening;

    private void OnEnable()
    {
        if (sceneManager != null)
        {
            sceneManager.onStartLoadScene += OnStartLoadScene;
            sceneManager.onLastFrameBeforeLoadScene += OnLastFrameBeforeLoadScene;
            sceneManager.onLoadProgressChange += OnLoadProgressChange;
            sceneManager.onFirstScene += OnFirstScene;
            listening = true;
        }
    }

    public void SetSceneManager(GameObjectVariable value)
    {
        if (sceneManager != null)
        {
            sceneManager.onStartLoadScene -= OnStartLoadScene;
            sceneManager.onLastFrameBeforeLoadScene -= OnLastFrameBeforeLoadScene;
            sceneManager.onLoadProgressChange -= OnLoadProgressChange;
            sceneManager.onFirstScene -= OnFirstScene;
            listening = false;
        }
        if (value.Value != null)
        {
            sceneManager = value.Value.GetComponent<SceneManager>();
            sceneManager.onStartLoadScene += OnStartLoadScene;
            sceneManager.onLastFrameBeforeLoadScene += OnLastFrameBeforeLoadScene;
            sceneManager.onLoadProgressChange += OnLoadProgressChange;
            sceneManager.onFirstScene += OnFirstScene;
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

    void OnLoadProgressChange(float value)
    {
        if (enabled)
        {
            onLoadProgressChange.Invoke(value);
        }
    }

    void OnFirstScene()
    {
        if (enabled)
        {
            onFirstScene.Invoke();
        }
    }

    private void OnDisable()
    {
        if (onStartLoadScene != null && listening && sceneManager != null)
        {
            sceneManager.onStartLoadScene -= OnStartLoadScene;
            sceneManager.onLastFrameBeforeLoadScene -= OnLastFrameBeforeLoadScene;
            sceneManager.onLoadProgressChange -= OnLoadProgressChange;
            sceneManager.onFirstScene -= OnFirstScene;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
