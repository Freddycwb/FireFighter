using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolAnimatorParameter : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetBoolTrue(string value)
    {
        animator.SetBool(value, true);
    }

    public void SetBoolFalse(string value)
    {
        animator.SetBool(value, false);
    }

    public void SwitchBoolValue(string value)
    {
        animator.SetBool(value, !animator.GetBool(value));
    }
}
