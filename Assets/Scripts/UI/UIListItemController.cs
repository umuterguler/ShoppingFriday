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
    public class UIListItemController : MonoBehaviour
    {       
        public Text TextSort;
        public Text TextName;
        public Text TextPoint;

        private void Start()
        {
                
        }

        public void SetListItemText(ListItemModel listItemModel)
        {
            TextSort.color = listItemModel.PlayerColor;
            TextName.color = listItemModel.PlayerColor;
            TextPoint.color = listItemModel.PlayerColor;

            TextSort.text = SortAbbreviation(listItemModel.SortNumber);           
            TextName.text = listItemModel.PlayerName;
            TextPoint.text = listItemModel.Point.ToString();

  

            if (!listItemModel.isNpc)
            {
                transform.GetComponent<Image>().color = new Color32(147, 121, 89, 255);//orange
            }
        }


        private string SortAbbreviation(int sortNumber)
        {
            string Abbreviation;
            if (sortNumber == 1)
            {
                Abbreviation = "1st";
                //transform.GetComponent<Image>().color = new Color32(253, 255, 0, 65);//gold
            }
            else if (sortNumber == 2)
            {
                Abbreviation = "2nd";
                //transform.GetComponent<Image>().color = new Color32(154, 15, 151, 55);//gold
            }
            else if (sortNumber == 3)
            {
                Abbreviation = "3rd";
                //transform.GetComponent<Image>().color = new Color32(206, 145, 70, 45);//gold
            }
            else
            {
                Abbreviation = sortNumber + "th";
            }
            return Abbreviation;
        }



    }
}
