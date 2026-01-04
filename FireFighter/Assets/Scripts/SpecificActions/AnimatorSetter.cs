using UnityEngine;

public class AnimatorSetter : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetAnimationToEnd()
    {
        AnimatorClipInfo[] animatorinfo = animator.GetCurrentAnimatorClipInfo(0);
        string animationName = (animatorinfo[0].clip.name);
        animator.Play(animationName, -1, 1);
    }
}
