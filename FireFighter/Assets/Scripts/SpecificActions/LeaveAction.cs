using System;
using UnityEngine;

public class LeaveAction : MonoBehaviour
{
    public Action<GameObject> leave;

    private void OnDisable()
    {
        leave?.Invoke(gameObject);
    }
}
