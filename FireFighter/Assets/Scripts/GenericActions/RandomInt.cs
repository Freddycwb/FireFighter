using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomInt : MonoBehaviour
{
    [SerializeField] private int min;
    [SerializeField] private int max;

    [SerializeField] private UnityEvent<int> onRandomize;
    
    public void Randomize()
    {
        int i = Random.Range(min, max);
        onRandomize.Invoke(i);
    }
}
