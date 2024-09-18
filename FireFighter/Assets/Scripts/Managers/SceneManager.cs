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


    private void Start()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadScene(currentScene));
    }

    private IEnumerator LoadScene(string value)
    {
        if (onStartLoadScene != null)
        {
            onStartLoadScene(value);
        }
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }
}
