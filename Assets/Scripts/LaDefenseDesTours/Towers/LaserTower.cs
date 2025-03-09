using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserTower : Tower
    {
        private readonly float damageOverTime = 1f;

        public LaserTower()
        {
            specialAbility = damageOverTime;
        }
    }
}
