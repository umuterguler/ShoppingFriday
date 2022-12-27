using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIStarsController : MonoBehaviour
    {
        public enum StarCountEnum
        {
            ZeroStar,
            OneStar,
            TwoStar,
            ThreeStar
        }

        public Image[] Stars;        

        private void Start()
        {
              
        }                          

        public void SetMyStars(StarCountEnum starCountEnum)
        {
            switch (starCountEnum)
            {
                case StarCountEnum.ZeroStar:
                    Stars[0].enabled = false;
                    Stars[1].enabled = false;
                    Stars[2].enabled = false;
                    break;
                case StarCountEnum.OneStar:
                    Stars[0].enabled = true;
                    break;
                case StarCountEnum.TwoStar:
                    Stars[0].enabled = true;
                    Stars[1].enabled = true;
                    break;
                case StarCountEnum.ThreeStar:
                    Stars[0].enabled = true;
                    Stars[1].enabled = true;
                    Stars[2].enabled = true;
                    break;
                default:
                    break;
            }          
        }
                                 
        
    }



}
