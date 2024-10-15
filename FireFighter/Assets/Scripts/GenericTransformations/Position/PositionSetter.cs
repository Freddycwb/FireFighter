using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSetter : MonoBehaviour
{
    public void SetPosition(GameObject value)
    {
        transform.position = value.transform.position;
    }

    public void SetPosition(GameObjectVariable value)
    {
        transform.position = value.Value.transform.position;
    }
}
