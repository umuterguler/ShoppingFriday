using Assets.Scripts.Data.Enums;
using Assets.Scripts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIPanelTopController : MonoBehaviour
    {
        [Header("TopBar")]
        public Text TextBasketCount;
        public GameObject FullObject;
        public Text TextCountdown;


        public GameObject LeaderBoardObject;
        public GameObject[] LeaderBoardItems;


        private void Update()
        {

            if (ScoreManager.Instance.levelStatus == LevelStatusEnum.Continues)
            {
                Countdown();
                LeaderBoardVisibility(true);
            }
        }

        private float LevelCountDownTime;



        public void SetBasketCounter(int count)
        {
            TextBasketCount.text = count.ToString();
        }

        public void FullSignVisiblity(bool statu)
        {
            FullObject.SetActive(statu);
        }

        public void SetLevelTime(float countdown)
        {
            if (countdown >= 0)
            {
                TimeSpan time = TimeSpan.FromSeconds(countdown);
                DateTime dateTime = DateTime.Today.Add(time);
                string displayTime = dateTime.ToString("mm:ss");



                LevelCountDownTime = countdown;
                TextCountdown.text = displayTime;
            }
        }

        public float GetLevelTime()
        {
            return LevelCountDownTime;
        }

        private void Countdown()
        {
            if (LevelCountDownTime > 0)
            {
                LevelCountDownTime -= Time.deltaTime;
                SetLevelTime(LevelCountDownTime);
            }
            else
            {
                //LevelTime doldu...
                ScoreManager.Instance.LevelFinish();
            }
        }


        public void PrepareLeaderBoard(PlayerModel[] playerModels)
        {
            for (int i = 0; i < playerModels.Length; i++)
            {
                if(i == 3 && playerModels[3] == null)
                {                   
                    LeaderBoardItems[3].GetComponent<UILeaderItemController>().SetItemVisibility(false);
                }
                else
                {
                    ListItemModel listItemModel = new ListItemModel();
                    listItemModel.SortNumber = playerModels[i].SortOrderNumber;
                    listItemModel.PlayerName = playerModels[i].PlayerName;
                    listItemModel.Point = playerModels[i].CollectedItem;
                    listItemModel.isNpc = playerModels[i].isNpc;
                    listItemModel.PlayerColor = playerModels[i].PlayerColor;

                    LeaderBoardItems[3].GetComponent<UILeaderItemController>().SetItemVisibility(true);

                    LeaderBoardItems[i].GetComponent<UILeaderItemController>().SetListItemText(listItemModel);
                }       
            }
        }

        public void LeaderBoardVisibility(bool statu)
        {
            LeaderBoardObject.SetActive(statu);
        }

    }
}
