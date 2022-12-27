using Assets.Scripts.Controllers;
using Assets.Scripts.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class HumanIdlesBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            //int basketCount =  animator.GetComponent<PublicController>().BasketObject.GetComponent<BasketController>().GetBasketCount();

            animator.SetInteger("IdleIndex", UnityEngine.Random.Range(1, 7));

            //  animator.SetInteger("IdleIndex", 1);
            //if (basketCount > 0)
            //{
            //    animator.SetInteger("IdleIndex", 0);
            //}
            //else
            //{
            //    animator.SetInteger("IdleIndex", UnityEngine.Random.Range(1, 7));
            //}

        }


    }
}
