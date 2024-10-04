using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManagerEvents : MonoBehaviour
{
    [SerializeField] private SceneManager sceneManager;

    [SerializeField] private UnityEvent onStartLoadScene;

    private bool listening;

    private void OnEnable()
    {
        if (sceneManager != null)
        {
            sceneManager.onStartLoadScene += OnStartLoadScene;
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

    private void OnDisable()
    {
        if (onStartLoadScene != null && listening)
        {
            sceneManager.onStartLoadScene -= OnStartLoadScene;
            listening = false;
        }
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
