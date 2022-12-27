using Assets.Scripts.Data.Enums;
using Assets.Scripts.Data.Models;
using Assets.Scripts.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.UI.UIStarsController;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private PlayerPrefsManager playerPrefsManager;    
    private MainUI Main_UI;


    public LevelStatusEnum levelStatus = LevelStatusEnum.Ready;
    public GameObject FinalStage;
    public PlayerModel[] playerModels;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerPrefsManager = new PlayerPrefsManager();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Main_UI = FindObjectOfType<MainUI>();
        StartCurrentLevel();       
    }

    void Update()
    {
        if (GameManager.pausePlay == PausePlayEnum.Play)
        {
            if (levelStatus != LevelStatusEnum.Ready)
            {
            }            
        }
        else
        {
            // Pause...
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.pausePlay == PausePlayEnum.Play)
        {
            if (levelStatus != LevelStatusEnum.Ready)
            {
                Main_UI.PrepareLeaderBoard(SortingContinues());
            }
        }
        else
        {
            // Pause...
        }
    }


    public void StartCurrentLevel()
    {
        FinalStage.SetActive(false);
        Main_UI.SetLevelTime(LevelGenerator.Instance.GetCurrenLevelTime());
        levelStatus = LevelStatusEnum.Ready;
    }

    public void LevelContinues()
    {
        GameManager.pausePlay = PausePlayEnum.Play;
        levelStatus = LevelStatusEnum.Continues;             
    }

    public void LevelFinish()
    {
        // CalculateStars();    
        FinalStage.SetActive(true);
        Main_UI.OpenLevelFinishPanel(SortingFinish());
    }    
 

    public void CalculateStars()
    {

        //if (CollectedProduct < CalculateStarPercent(40))
        //{            
        //    // Fail Screen
        //    Main_UI.SetStars(StarCountEnum.ZeroStar);
        //    levelStatus = LevelStatusEnum.Fail;
        //}
        //else if (CollectedProduct >= CalculateStarPercent(40) && CollectedProduct < CalculateStarPercent(60))
        //{
        //    // Fail Screen
        //    Main_UI.SetStars(StarCountEnum.OneStar);
        //    levelStatus = LevelStatusEnum.Fail;
           
        //}
        //else if (CollectedProduct >= CalculateStarPercent(60) && CollectedProduct < CalculateStarPercent(80))
        //{
        //    Main_UI.SetStars(StarCountEnum.TwoStar);
        //    levelStatus = LevelStatusEnum.Win;
        //}
        //else if (CollectedProduct >= CalculateStarPercent(80))
        //{
        //    Main_UI.SetStars(StarCountEnum.ThreeStar);
        //    levelStatus = LevelStatusEnum.Win;
        //}    
    }
          

    private PlayerModel[] SortingContinues()
    {
        bool playerFounded = false;

        PlayerModel[] FourGuys = new PlayerModel[4];
        playerModels = playerModels.OrderByDescending(x => x.GetComponent<PlayerModel>().CollectedItem).ToArray();

        for (int i = 0; i < playerModels.Length; i++)
        {
            //Leader Crown Visiblity...
            if (i == 0)
            {                
                SetCrownVisibility(playerModels[i], true); //lider
            }
            else if (i > 0)
            {               
                SetCrownVisibility(playerModels[i], false); //lider değil
            }

            playerModels[i].SortOrderNumber = i + 1;

            if (i == 0)
            {
                if (!playerModels[i].isNpc)
                {
                    playerFounded = true;
                }               
                    
                FourGuys[0] = playerModels[i];

            }
            else if (i == 1)
            {
                if (!playerModels[i].isNpc)
                {
                    playerFounded = true;
                }
                FourGuys[1] = playerModels[i];

            }
            else if (i == 2 )
            {
                if (!playerModels[i].isNpc)
                {
                    playerFounded = true;
                }

                FourGuys[2] = playerModels[i];
            }
            else if (!playerModels[i].isNpc && !playerFounded)
            {
                //playerin ilk 3 te olmadığı durum...
                FourGuys[3] = playerModels[i];
            }
        }
        return FourGuys;
    }


    private PlayerModel[] SortingFinish()
    {
        playerModels = playerModels.OrderByDescending(x => x.GetComponent<PlayerModel>().CollectedItem).ToArray();

        for (int i = 0; i < playerModels.Length; i++)
        {          
            if (!playerModels[i].isNpc && i < 3)
            {
                levelStatus = LevelStatusEnum.Win;
            }
            else if (!playerModels[i].isNpc && i >= 3)
            {
                levelStatus = LevelStatusEnum.Fail;
            }

            playerModels[i].SortOrderNumber = i + 1;
        }

        return playerModels;
    }


    private void SetCrownVisibility(PlayerModel playerModel, bool isLeader)
    {
        if (playerModel.isNpc)
        {
            playerModel.GetComponent<PlayerNPCController>().SetCrownVisibility(isLeader);
        }
        else
        {
            playerModel.GetComponent<PlayerController>().SetCrownVisibility(isLeader);
        }
    }


   



}
