using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShader : MonoBehaviour
{
    public void SetTimeOffSet()
    {
        GetComponent<Renderer>().material.SetFloat("_TimeOffset", Time.time);
    }
}
