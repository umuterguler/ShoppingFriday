using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class PlayerPrefsManager
    {

        public void SetCurrentLevel(int levelIndex)
        {
            PlayerPrefs.SetInt("current_level", levelIndex);
        }

        public int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt("current_level", 0);
        }
        

       


    }
}
