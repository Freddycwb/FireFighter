using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private string _currentScene;
    private float _loadProgress;

    [SerializeField] private float delay;
    [SerializeField] private BoolVariable firstScene;

    public Action<string> onStartLoadScene;
    public Action onLastFrameBeforeLoadScene;
    public Action<float> onLoadProgressChange;
    public Action onFirstScene;
    public Action<string> onFinishLoadingAdditiveScene;

    [SerializeField] private List<string> sceneLoaded = new List<string>(); 


    private void Start()
    {
        _currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (firstScene.Value)
        {
            firstScene.Value = false;
            if (onFirstScene != null)
            {
                onFirstScene.Invoke();
            }
        }
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadSceneRoutine(_currentScene));
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
        
        _currentScene = value;

        if (onLastFrameBeforeLoadScene != null)
        {
            onLastFrameBeforeLoadScene.Invoke();
        }
        yield return new WaitForEndOfFrame();

        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_currentScene);
        while (!operation.isDone)
        {
            if (_loadProgress != Mathf.Clamp01(operation.progress / 0.9f))
            {
                if (onLoadProgressChange != null)
                {
                    onLoadProgressChange.Invoke(Mathf.Clamp01(operation.progress / 0.9f));
                }
            }
            _loadProgress = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
    }

    public void LoadSceneAdditive(string value)
    {
        StartCoroutine(LoadSceneAdditiveRoutine(value));
    }

    private IEnumerator LoadSceneAdditiveRoutine(string value)
    {
        if (!sceneLoaded.Contains(value))
        {
            _currentScene = value;
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_currentScene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            while (!operation.isDone)
            {
                yield return null;
            }
            onFinishLoadingAdditiveScene?.Invoke(value);
            sceneLoaded.Add(value);
        }
    }

    public void UnloadScene(string value)
    {
        StartCoroutine(UnloadSceneRoutine(value));
    }

    private IEnumerator UnloadSceneRoutine(string value)
    {
        if (sceneLoaded.Contains(value))
        {
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(value);
            while (!operation.isDone)
            {
                yield return null;
            }
            sceneLoaded.Remove(value);
        }
    }

    void OnApplicationQuit()
    {
        firstScene.Value = true;
    }
}
