using Assets.Scripts.LaDefenseDesTours.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Towers.Data
{

    [CreateAssetMenu(fileName = "TowerData.asset", menuName = "La défense des tours/Tower Configuration", order = 1)]
    public class TowerData : ScriptableObject
    {

        public string towerName;
        public string description;
        public string upgradeDescription;
        public int cost;
        public int dps;
        public int sellCost;
        public int currentLevel;
        public float range;
        public Sprite icon;
        
    }

}
