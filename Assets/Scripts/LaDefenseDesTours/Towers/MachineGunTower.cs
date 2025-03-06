using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunTower : Tower
    {
        public float attackPerSecond { get; set; } = 1.5f;

        public MachineGunTower()
        {
        }
      
    }
}
