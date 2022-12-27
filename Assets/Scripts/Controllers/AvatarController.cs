using Assets.Scripts.Data.Enums;
using Assets.Scripts.Data.Models;
using Assets.Scripts.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class AvatarController : MonoBehaviour
    {
        private PlayerAnimation playerAnimation;
        private GameObject playerObject;

        public GameObject BasketObject;

        int frameCount;


        private void Awake()
        {
            playerObject = transform.parent.gameObject;
            playerAnimation = new PlayerAnimation();
        }

        void Start()
        {
            playerAnimation.Init(transform.GetComponent<Animator>());
        }



        private void Update()
        {
            // float speed = Vector3.Distance(current_pos, last_pos) / Time.deltaTime;     
            Rigidbody rb = playerObject.transform.GetComponent<Rigidbody>();
            float magnitude = Vector3.Project(rb.velocity, transform.forward).magnitude;

            if (magnitude > 1f )
            {               
                PlayerAnimationRun();
            }
            else 
            {
                if (ScoreManager.Instance.levelStatus != LevelStatusEnum.Win && ScoreManager.Instance.levelStatus != LevelStatusEnum.Fail)
                {                 
                    PlayerAnimationIdle();
                }

                if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Win)
                {
                    PlayerAnimationDance();
                }
                else if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Fail)
                {
                    PlayerAnimationFail();
                }                     
            }
        }               

        public void PlayerAnimationRun()
        {
            playerAnimation.SetActionStatus(PlayerAnimationEnum.Run);

            if (BasketObject.GetComponent<BasketController>().GetBasketCount() > 0)
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
            if (BasketObject.GetComponent<BasketController>().GetBasketCount() > 0)
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
            playerAnimation.SetActionStatus(PlayerAnimationEnum.Run);
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


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constant.TAG_OBSTACLE))
            {
                if (other.gameObject.GetComponent<ObstacleModel>().ObstacleType == ObstacleTypeEnum.Wetfloor)
                {
                    //Ellerin boşken düşme
                    if (BasketObject.GetComponent<BasketController>().GetBasketCount() > 0)
                    {
                        ///WetFloor 1 sn tekrar düşmesin
                        BasketObject.GetComponent<BasketController>().CreateFallItems();

                        BasketObject.GetComponent<BasketController>().HideBasket();

                        PlayerAnimationFallUp();
                        playerObject.GetComponent<PlayerController>().playerStatus = PlayerStatusEnum.Unavailable;
                    }

                                 

                }
            }
        }


        public void OutBasketObject()
        {
           
            BasketObject.transform.SetParent(playerObject.transform);
            BasketObject.transform.localPosition = new Vector3(-0.19f, -0.24f, 4.54f);

        }









    }
}
