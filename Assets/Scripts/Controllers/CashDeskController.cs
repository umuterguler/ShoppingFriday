using Assets.Scripts.Data.Enums;
using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashDeskController : MonoBehaviour
{
    public GameObject cashDeskItems;

    void Start()
    {
    }

    void Update()
    {
        if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Win || ScoreManager.Instance.levelStatus == LevelStatusEnum.Fail)
        {
            RefreshDesk();
        }  
    }

    public void VisibleDeskProduct()
    {
        if (cashDeskItems.active == false)
        {
            cashDeskItems.SetActive(true);

            this.Wait(1f, () =>
            {
                cashDeskItems.SetActive(false);
            });

        }       
    }        
   

    private void RefreshDesk()
    {
        cashDeskItems.SetActive(false);
    }


    


}
