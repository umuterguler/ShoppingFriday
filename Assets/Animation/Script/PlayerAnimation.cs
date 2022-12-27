using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    //private int actionStatus =  Animator.StringToHash("ActionStatus");
   // private int fall = Animator.StringToHash("Fall");


    public void Init(Animator anim)
    {
        animator = anim;
    }


    public void SetActionStatus(PlayerAnimationEnum actionStatusEnum)
    {
        animator.SetInteger(Animator.StringToHash("ActionStatus"), (int)actionStatusEnum);
    }


    public void SetRunIndex(int runIndex)
    {
        animator.SetInteger(Animator.StringToHash("RunIndex"), (int)runIndex);
    }


    public void SetRunSpeed(float _runSpeed)
    {
        animator.speed = _runSpeed;
    }

    public void Fall()
    {
        animator.SetTrigger("Fall");
    }


}
