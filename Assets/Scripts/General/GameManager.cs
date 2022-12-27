using Assets.Scripts.Data.Enums;
using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerPrefsManager playerPrefsManager;
    public static PausePlayEnum pausePlay;

    void Awake()
    {
        if (Instance == null)
        {
           // DontDestroyOnLoad(gameObject);
            Instance = this;
            playerPrefsManager = new PlayerPrefsManager();
            pausePlay = PausePlayEnum.Play;


            /// <summary>
            /// TEST -- TEST -- TEST -- TEST -- TEST -- TEST -- TEST
            /// </summary>
            // ResetUserInfo();
            //playerPrefsManager.SetCoinBalance(5500);

            //playerPrefsManager.SetCurrentLevel(4);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void PauseGame()
    {
        pausePlay = PausePlayEnum.Pause;
        //Time.timeScale = 0f;
        //AudioListener.pause = true;
        //print("pause");
    }

    public void ResumeGame()
    {
        pausePlay = PausePlayEnum.Play;
        // Time.timeScale = 1;
        AudioListener.pause = false;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }


}
