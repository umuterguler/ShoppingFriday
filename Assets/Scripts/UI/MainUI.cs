using Assets.Scripts.Controllers;
using Assets.Scripts.Data.Enums;
using Assets.Scripts.Data.Models;
using Assets.Scripts.General;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Assets.Scripts.UI.UIStarsController;

public class MainUI : MonoBehaviour
{
    private PlayerPrefsManager playerPrefsManager;

    public GameObject PanelStart;
       

    [Header("FinishPanel")]
    public GameObject PanelFinish;
   // public GameObject Stars;
    public Text TextLevelResult;
        
    public GameObject ListItemParent;
    public GameObject ListItem;

    public GameObject ButtonNextLevel;
    public GameObject ButtonRetry;

    public ParticleSystem ConfettiFinish_1;
    public ParticleSystem ConfettiFinish_2;
   

    [Header("Panels")]
    public GameObject PanelTop;
    private UIPanelTopController UIPanelTop;


    private void Awake()
    {
        UIPanelTop = PanelTop.GetComponent<UIPanelTopController>();
    }

    void Start()
    {
        playerPrefsManager = new PlayerPrefsManager();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {          
            if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Ready)
            {
                ScoreManager.Instance.LevelContinues();
                PanelStart.SetActive(false);
            }  
        }      
    }
       

    #region PANEL TOP - PANEL TOP - PANEL TOP - PANEL TOP - PANEL TOP 

    public void SetBasketCounter(int count)
    {
        UIPanelTop.SetBasketCounter(count);
    }

    public void FullSignVisiblity(bool statu)
    {
        UIPanelTop.FullSignVisiblity(statu);
    }

    public void SetLevelTime(float countdown)
    {
        UIPanelTop.SetLevelTime(countdown);
    }

    public float GetLevelTime()
    {
        return UIPanelTop.GetLevelTime();
    }
       
    public void PrepareLeaderBoard(PlayerModel[] playerModels)
    {
        UIPanelTop.PrepareLeaderBoard(playerModels);
    }
       

    #endregion


    #region PANEL FINISH - PANEL FINISH - PANEL FINISH - PANEL FINISH - PANEL FINISH 

    public void OpenLevelFinishPanel(PlayerModel[] playerModels)
    {
        PreparePlayerList(playerModels);
        CelebrationsStatus();
        this.Wait(5f, () =>
        {
            PanelFinishVisiblity(true);            
            FinishButtonSetup();
        });
    }

    public void SetStars(StarCountEnum starCountEnum)
    {
       // Stars.GetComponent<UIStarsController>().SetMyStars(starCountEnum);
    }


    //private void SetCollectedProduct(int product)
    //{
    //    TextProduct.text = ((int)product).ToString();
    //}

                       

    private void PanelFinishVisiblity(bool status)
    {
        PanelFinish.SetActive(status);
    }

    private void FinishButtonSetup()
    {
        if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Win)
        {
            TextLevelResult.text = "EXCELLENT!";
            ButtonNextLevel.SetActive(true);
            ButtonRetry.SetActive(false);
        }
        else if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Fail)
        {
            TextLevelResult.text = "FAILED!";
            ButtonNextLevel.SetActive(false);
            ButtonRetry.SetActive(true);
        }
    }


    public void ClickRetryLevel()
    {        
        this.Wait(1f, () =>
        {
            //LevelGenerator.Instance.NextLevel();
            PanelFinishVisiblity(false);
            ScoreManager.Instance.StartCurrentLevel();
            PanelStart.SetActive(true);
        });
        SceneManager.LoadScene(0);
    }

    public void CelebrationsStatus()
    {
        if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Win)
        {
            ConfettiFinish_1.Play();
            ConfettiFinish_2.Play();
        }
        else
        {
            ConfettiFinish_1.Stop();
            ConfettiFinish_2.Stop();
        }
    }
    

    private void PreparePlayerList(PlayerModel[] playerModels)
    {
        for (int i = 0; i < playerModels.Length; i++)
        {
            GameObject listItemGo = Instantiate(ListItem, ListItemParent.transform);
            ListItemModel listItemModel = new ListItemModel();
            listItemModel.SortNumber = playerModels[i].SortOrderNumber;
            listItemModel.PlayerName = playerModels[i].PlayerName;
            listItemModel.Point = playerModels[i].CollectedItem;
            listItemModel.isNpc = playerModels[i].isNpc;
            listItemModel.PlayerColor = playerModels[i].PlayerColor;
            listItemGo.GetComponent<UIListItemController>().SetListItemText(listItemModel);            
        } 
    }

    #endregion




    











}
