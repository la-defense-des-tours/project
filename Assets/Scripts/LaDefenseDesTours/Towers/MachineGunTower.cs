using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunTower : Tower
    {
        private readonly float attackPerSecond = 1.5f;
        
        public MachineGunTower()
        {
            specialAbility = attackPerSecond;
        }
    }
}
