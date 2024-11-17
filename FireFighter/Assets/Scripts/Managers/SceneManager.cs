using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SceneManager : MonoBehaviour
{
    private string currentScene;

    [SerializeField] private float delay;

    public Action<string> onStartLoadScene;
    public Action onLastFrameBeforeLoadScene;


    private void Start()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadSceneRoutine(currentScene));
    }

    public void LoadScene(string value)
    {
        StartCoroutine(LoadSceneRoutine(value));
    }

    private IEnumerator LoadSceneRoutine(string value)
    {
        if (onStartLoadScene != null)
        {
            onStartLoadScene(value);
        }
        yield return new WaitForSecondsRealtime(delay);
        currentScene = value;
        if (onLastFrameBeforeLoadScene != null)
        {
            onLastFrameBeforeLoadScene.Invoke();
        }
        yield return new WaitForEndOfFrame();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }
}
