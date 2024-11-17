using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Replay(string value)
    {
        animator.Play(value, 0, 0f);
    }
}
