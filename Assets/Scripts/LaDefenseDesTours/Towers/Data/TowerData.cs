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
        /// <summary>
        /// A description of the tower for displaying on the UI
        /// </summary>
        public string description;

        /// <summary>
        /// A description of the tower for displaying on the UI
        /// </summary>
        public string upgradeDescription;


        /// <summary>
        /// Price of the tower
        /// </summary>
        public int cost;

        ///// <summary>
        ///// The tower levels associated with this tower
        ///// </summary>
        //public TowerLevel[] levels;


        /// <summary>
        /// range of the tower
        /// </summary>
        public float range;

        /// <summary>
        /// The tower icon
        /// </summary>
        public Sprite icon;
    }

}
