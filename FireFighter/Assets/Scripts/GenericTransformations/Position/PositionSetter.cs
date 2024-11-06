using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSetter : MonoBehaviour
{
    public void SetPosition(GameObject value)
    {
        if (value != null)
        {
            transform.position = value.transform.position;
        }
    }

    public void SetPosition(GameObjectVariable value)
    {
        if (value != null && value.Value != null)
        {
            transform.position = value.Value.transform.position;
        }
    }
}
