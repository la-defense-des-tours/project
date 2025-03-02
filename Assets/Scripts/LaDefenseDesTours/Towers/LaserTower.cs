using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserTower : Tower
    {
        public float damageOverTime { get; set; } = 1f;

        public LaserTower()
        {
            currentLevel = 1;
            range = 50f;
            damage = 25f;
            specialAbility = damageOverTime;
        }
        public override void Start()
        {
            if (isGhost) return;
            base.Start();
        }
       
    }
}
