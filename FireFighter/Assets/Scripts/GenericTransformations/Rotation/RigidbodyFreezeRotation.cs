using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyFreezeRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public void Freeze()
    {
        rb.freezeRotation = true;
    }

    public void Unfreeze()
    {
        rb.freezeRotation = false;
    }
}
