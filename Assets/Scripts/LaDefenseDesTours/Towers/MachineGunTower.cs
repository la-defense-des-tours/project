using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunTower : Tower
    {
        private NavMeshAgent agent;

        public override string  towerName { get; } = "Machine Gun Tower";
        private float attackPerSecond { get; set; }
        public override void Upgrade()
        {
            currentLevel++;
            health += 10;
            cost += 10;
            attackPerSecond += 0.5f;
        }


        public override void Attack()
        {
            Debug.Log("Machine Gun Tower is attacking");
        }
    }
}
