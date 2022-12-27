using Assets.Scripts.Data.Enums;
using Assets.Scripts.Data.Models;
using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    private int BasketItemCount = 0;
    private int CollectedItemCount = 0;

    public GameObject FallItemsParent;
    public GameObject FollowObject;
    public GameObject FallItemsPool;

    public GameObject TextPointParent;
    public TextMeshPro TextPoint;

    private MainUI Main_UI;


    void Start()
    {
        Main_UI = FindObjectOfType<MainUI>();
        BasketItemCount = 0;
        CollectedItemCount = 0;

    }

    void Update()
    {
        if (BasketItemCount > 0 && (ScoreManager.Instance.levelStatus == LevelStatusEnum.Win || ScoreManager.Instance.levelStatus == LevelStatusEnum.Fail))
        {
            //ClearBasket();
        }       

    }

    public void AddBasketCount()
    {        
        CollectedItemCount = CollectedItemCount + 3;
        BasketItemCount = BasketItemCount + 3;
        transform.GetComponentInParent<PlayerModel>().CollectedItem = CollectedItemCount;
        BasketItemAdd();
        Main_UI.SetBasketCounter(CollectedItemCount);
        // TextPoint Tetiklenecek.....
        TextMeshPro slogan = Instantiate(TextPoint, TextPointParent.transform);
        slogan.text = "+3";
        slogan.color = Color.green;
        Destroy(slogan.gameObject, 1f);
    }

    public void RemoveBasketCount(int removeNumber)
    {
        if (BasketItemCount > 0)
        {
            BasketItemRemove(removeNumber);

            if (BasketItemCount > removeNumber)
            {
                CollectedItemCount = CollectedItemCount - removeNumber;
                Main_UI.SetBasketCounter(CollectedItemCount);
                BasketItemCount = BasketItemCount - removeNumber;
                transform.GetComponentInParent<PlayerModel>().CollectedItem = transform.GetComponentInParent<PlayerModel>().CollectedItem - removeNumber;
            }
            else
            {
                CollectedItemCount = CollectedItemCount - BasketItemCount;
                Main_UI.SetBasketCounter(CollectedItemCount);
                BasketItemCount = 0;
                transform.GetComponentInParent<PlayerModel>().CollectedItem = CollectedItemCount;
            }

            TextMeshPro slogan = Instantiate(TextPoint, TextPointParent.transform);
            slogan.text = "-"+ Constant.FallItemNumber.ToString();
            slogan.color = Color.red;
            Destroy(slogan.gameObject, 1f);
        }
    }

    public int GetBasketCount()
    {
        return BasketItemCount;
    }


    private void BasketItemAdd()
    {
        //transform.GetChild(BasketCounter - 3).gameObject.SetActive(true);
        transform.GetChild(BasketItemCount - 2).gameObject.SetActive(true);
        transform.GetChild(BasketItemCount - 1).gameObject.SetActive(true);
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

    public void HideBasket()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowBasket()
    {
        this.gameObject.SetActive(true);
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
        for (int x = 0; x < Constant.FallItemNumber + 5; x++)
        {
            Instantiate(RandomFallItems(), FallItemsParent.transform.position, Quaternion.identity, FallItemsParent.transform);
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

    private GameObject RandomFallItems()
    {
        int number = Random.Range(1, FallItemsPool.transform.childCount);

        return FallItemsPool.transform.GetChild(number).gameObject;
    }



    private void DropToCashDesk()
    {
        //BasketItemRemove(BasketItemCount);
        BasketItemRemove(BasketItemCount);
        BasketItemCount = 0;        
    }

    




}
