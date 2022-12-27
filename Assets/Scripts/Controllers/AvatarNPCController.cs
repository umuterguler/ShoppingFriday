using Assets.Scripts.Data.Enums;
using Assets.Scripts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Controllers
{
    public class AvatarNPCController : MonoBehaviour
    {
        private PlayerAnimation playerAnimation;
        private GameObject npcObject;
        private NavMeshAgent myNavAgent;
        public GameObject BasketObject;


        private void Awake()
        {
            npcObject = transform.parent.gameObject;
            playerAnimation = new PlayerAnimation();
        }

        void Start()
        {
            playerAnimation.Init(transform.GetComponent<Animator>());
            myNavAgent = transform.GetComponentInParent<NavMeshAgent>();
        }

        void Update()
        {
            float magnitud = Vector3.Project(myNavAgent.desiredVelocity, transform.forward).magnitude;
            
            if (magnitud > 1)
            {
                PlayerAnimationRun();
            }
            else
            {
                PlayerAnimationIdle();
            }
        }

        public void PlayerAnimationRun()
        {
            playerAnimation.SetActionStatus(PlayerAnimationEnum.Run);
            if (BasketObject.GetComponent<BasketControllerNPC>().GetBasketCount() > 0)
            {
                playerAnimation.SetRunIndex(2);
            }
            else
            {
                playerAnimation.SetRunIndex(1);
            }
        }

        public void PlayerAnimationIdle()
        {         
            if (BasketObject.GetComponent<BasketControllerNPC>().GetBasketCount() > 0)
            {
                playerAnimation.SetActionStatus(PlayerAnimationEnum.IdleBox);
            }
            else
            {
                playerAnimation.SetActionStatus(PlayerAnimationEnum.Idle);
            }
        }

        public void PlayerAnimationWalk()
        {
            playerAnimation.SetActionStatus(PlayerAnimationEnum.Walk);
        }

        public void PlayerAnimationDance()
        {
            playerAnimation.SetActionStatus(PlayerAnimationEnum.Dance);
        }

        public void PlayerAnimationFail()
        {
            playerAnimation.SetActionStatus(PlayerAnimationEnum.Fail);
        }

        public void PlayerAnimationFallUp()
        {
            playerAnimation.Fall();
        }





    }
}
