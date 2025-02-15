using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonTower : Tower
    {

        public override string towerName { get; } = "Canon Tower";

        public float areaOfEffect { get; set; }
        public override void Upgrade()
        {
            currentLevel++;
            health += 15;
            cost += 25;
            areaOfEffect += 0.5f;
        }

        public override void Attack()
        {
            Debug.Log("Canon Tower is attacking");
        }
    }

}
