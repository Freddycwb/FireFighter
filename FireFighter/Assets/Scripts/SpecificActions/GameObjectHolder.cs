using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectHolder : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectInsured;

    public GameObject GetGameObject()
    {
        return gameObjectInsured;
    }
}
