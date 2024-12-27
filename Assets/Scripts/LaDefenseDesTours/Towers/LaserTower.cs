using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserTower : Tower
    {
        internal GameObject gameObject;

        public float damageOverTime { get; set; }
        public override void Upgrade()
        {
            level++;
            health += 50;
            price += 50;
            damageOverTime += 1.5f;
        }
        public override void Attack()
        {
            Debug.Log("Laser Tower is attacking");
        }
    }

}
