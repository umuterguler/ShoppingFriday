using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data.Models
{
    public class PlayerModel : MonoBehaviour
    {
        public string PlayerName;
        public Color PlayerColor;
        public float PlayerSpeed;
        public int CollectedItem;
        public int CashDeskTarget;//x kadar topladığında kasaya götür...
        public bool IsLeader;
        public bool isNpc;
        public int SortOrderNumber;



    }
}
