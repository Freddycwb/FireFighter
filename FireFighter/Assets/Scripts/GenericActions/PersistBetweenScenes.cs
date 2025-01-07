using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersistBetweenScenes : MonoBehaviour
{
    public Action onObjDontDestroy;

    void Start()
    {
        PersistBetweenScenes[] objs = GameObject.FindObjectsByType<PersistBetweenScenes>(FindObjectsSortMode.None);

        foreach (PersistBetweenScenes obj in objs)
        {
            if (obj.name == name)
            {
                Destroy(this.gameObject);
            }
        }
        transform.SetParent(null);
        DontDestroyOnLoad(this.gameObject);
        if (onObjDontDestroy != null)
        {
            onObjDontDestroy.Invoke();
        }
    }
}
