using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterAnimation : InvokeAfter
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationName;
    private bool _isFinished = true;

    public void SetAnimationName(string value)
    {
        animationName = value;
    }

    public void SetAnimationNameToCurrent()
    {
        AnimatorClipInfo[] animatorinfo = animator.GetCurrentAnimatorClipInfo(0);
        SetAnimationName(animatorinfo[0].clip.name);
    }

    private void Update()
    {
        if (animationName == "" || animator == null)
        {
            return;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            Debug.Log("animação tem nome mas não é a esperada");
            return;
        }
        Debug.Log((animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) + " | " + !_isFinished);
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_isFinished)
        {
            _isFinished = true;
            CallAction();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1 && _isFinished)
        {
            _isFinished = false;
            CallSubAction();
        }
    }
}
