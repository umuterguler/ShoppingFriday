using Assets.Scripts.Controllers;
using Assets.Scripts.Data.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanFallBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponentInParent<PlayerController>().playerStatus = PlayerStatusEnum.Available;

        animator.GetComponent<AvatarController>().BasketObject.GetComponent<BasketController>().DestroyFallItems();

        animator.GetComponent<AvatarController>().BasketObject.GetComponent<BasketController>().ShowBasket();

    }



}
