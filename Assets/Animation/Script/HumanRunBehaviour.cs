using Assets.Scripts.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Animation.Script
{
    public class HumanRunBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            int basketCount =  animator.GetComponent<AvatarController>().BasketObject.GetComponent<BasketController>().GetBasketCount();
            //animator.SetInteger("IdleIndex", UnityEngine.Random.Range(1, 1));  

            animator.SetInteger("IdleIndex", 1);

            //if (basketCount > 0 )
            //{
            //    animator.SetInteger("IdleIndex", 2);
            //}
            //else
            //{
            //    animator.SetInteger("IdleIndex", 1);
            //}  
        }


    }
}
