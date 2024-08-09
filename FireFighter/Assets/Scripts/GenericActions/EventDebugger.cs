using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDebugger : MonoBehaviour
{
    public void WriteDebug(string value)
    {
        Debug.Log(gameObject.name + " : " + value);
    }

    public void WriteErrorDebug(string value)
    {
        Debug.Log(gameObject.name + " : " + value);
    }
}
