using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserTower : Tower
    {
        public override string towerName { get; } = "Laser Tower";
        public float damageOverTime { get; set; }
        public override float range { get; } = 50f;
        public override float damage {get; } = 0.5f;
        
        public override void Upgrade()
        {
            currentLevel++;
            health += 50;
            cost += 50;
            damageOverTime += 1.5f;
        }
        public override void Attack()
        {
            Debug.Log("Laser Tower is attacking");
        }
    }
}
