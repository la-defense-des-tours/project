using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserTower : Tower
    {
        private float damageOverTime { get; set; } = 1f;

        public LaserTower()
        {
            specialAbility = damageOverTime;
        }
    }
}
