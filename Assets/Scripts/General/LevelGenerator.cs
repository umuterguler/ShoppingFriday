using Assets.Scripts.General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance { get; private set; }

    public LevelModel[] LevelModelList;
    private PlayerPrefsManager playerPrefsManager;
    private GameObject[] shelf_list;

    //Level Instantiate Path
    public GameObject[] LevelList;
    public GameObject LevelParent;

    private void Awake()
    {
        //Application.targetFrameRate = 60;

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
        InstantiateNewLevel();

    }

    void Update()
    {
        
    }



    public string GetCurrentLevel()
    {
        int currentLevelIndex = playerPrefsManager.GetCurrentLevel();
        return "Level " + (currentLevelIndex + 1).ToString();
    }    

    public float GetCurrenLevelTime()
    {
        int currentLevelIndex = playerPrefsManager.GetCurrentLevel();
        if (currentLevelIndex <= LevelModelList.Length)
        {             
            return LevelModelList[currentLevelIndex].LevelTime;
        }
        else
        {
            //Son levela ulaşıldı...
            print("Last level...");
            return 0;
        }
    }





    public void CalculateLevelGroup()
    {
        int level = playerPrefsManager.GetCurrentLevel();

        if (level > 1 && level < 6)
        {
            //group 1
        }
        else if (level > 5 && level < 11)
        {
            //group 2
        }
        else if (level > 10 && level < 16)
        {
            //group 3
        }
        else if (level > 17 && level < 21)
        {
            //group 4
        }

    }

    public void InstantiateNewLevel()
    {
        if (playerPrefsManager.GetCurrentLevel() > 0)
        {
            Destroy(LevelParent.transform.GetChild(0).gameObject);

            this.Wait(0.01f, () =>
            {
                Instantiate(LevelList[playerPrefsManager.GetCurrentLevel()], LevelParent.transform);
            });

        }
    }


}
