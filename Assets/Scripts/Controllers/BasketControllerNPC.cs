using Assets.Scripts.Data.Enums;
using Assets.Scripts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class BasketControllerNPC : MonoBehaviour
    {

        private int BasketItemCount = 0;
        private int CollectedItemCount = 0;

        public GameObject FallItemsParent;
        public GameObject FollowObject;
        public GameObject FallItemsPool;
        


        void Start()
        {
            BasketItemCount = 0;
            CollectedItemCount = 0;
        }

        void Update()
        {
            if (BasketItemCount > 0 && (ScoreManager.Instance.levelStatus == LevelStatusEnum.Win || ScoreManager.Instance.levelStatus == LevelStatusEnum.Fail))
            {
                
            }
        }

        public void AddBasketCount()
        {
            CollectedItemCount = CollectedItemCount + 3;
            BasketItemCount = BasketItemCount + 3;
            transform.GetComponentInParent<PlayerModel>().CollectedItem = CollectedItemCount;
            BasketItemAdd();

            if (BasketItemCount >= transform.GetComponentInParent<PlayerModel>().CashDeskTarget)
            {
                transform.GetComponentInParent<PlayerNPCController>().ToCashDesk();
            }

        }


        public void RemoveBasketCount(int removeNumber)
        {
            if (BasketItemCount > 0)
            {
                BasketItemRemove(removeNumber);

                if (BasketItemCount > removeNumber)
                {
                    CollectedItemCount = CollectedItemCount - removeNumber;
                    BasketItemCount = BasketItemCount - removeNumber;
                    transform.GetComponentInParent<PlayerModel>().CollectedItem = transform.GetComponentInParent<PlayerModel>().CollectedItem - removeNumber;
                }
                else
                {
                    CollectedItemCount = CollectedItemCount - BasketItemCount;
                    BasketItemCount = 0;
                    transform.GetComponentInParent<PlayerModel>().CollectedItem = CollectedItemCount;
                }     
            }
        }

        public int GetBasketCount()
        {
            return BasketItemCount;
        }

        private void BasketItemAdd()
        {
            //transform.GetChild(BasketItemCount - 3).gameObject.SetActive(true);
            transform.GetChild(BasketItemCount - 2).gameObject.SetActive(true);
            transform.GetChild(BasketItemCount-1).gameObject.SetActive(true);   
            transform.GetChild(BasketItemCount).gameObject.SetActive(true);
        }

        private void BasketItemRemove(int fallItemCount)
        {
            if (BasketItemCount < fallItemCount)
            {
                for (int i = 0; i < BasketItemCount; i++)
                {
                    transform.GetChild(BasketItemCount - i).gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < fallItemCount; i++)
                {
                    transform.GetChild(BasketItemCount - i).gameObject.SetActive(false);
                }
            }
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constant.TAG_SHELF))
            {
                if (!other.gameObject.GetComponent<ShelfModel>().IsEmpty)
                {
                    AddBasketCount();
                    other.gameObject.GetComponent<ShelfModel>().IsEmpty = true;
                    other.gameObject.GetComponent<ShelfController>().PlayHopAnimation(FollowObject.transform);
                }
            }
            else if (other.gameObject.CompareTag(Constant.TAG_CASH_DESK))
            {
                if (BasketItemCount > 0)
                {
                    other.gameObject.GetComponent<CashDeskController>().VisibleDeskProduct();
                    DropToCashDesk();
                }               
            }
        }




        public void CreateFallItems()
        {           
            for (int x = 0; x < Constant.FallItemNumber; x++)
            {
                Instantiate(FallItemsPool, FallItemsParent.transform.position, Quaternion.identity, FallItemsParent.transform);
            }
            RemoveBasketCount(Constant.FallItemNumber);
        }

        public void DestroyFallItems()
        {
            for (int i = 0; i < FallItemsParent.transform.childCount; i++)
            {
                Destroy(FallItemsParent.transform.GetChild(i).gameObject);
            }
        }

        private void DropToCashDesk()
        {
            //BasketItemRemove(BasketItemCount);
            BasketItemRemove(BasketItemCount);
            BasketItemCount = 0;
            transform.GetComponentInParent<PlayerNPCController>().CashDeskDone();
        }

    }
}
