using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunTower : Tower
    {
        public float attackPerSecond { get; set; }
        public override void Upgrade()
        {
            level++;
            health += 10;
            price += 10;
            attackPerSecond += 0.5f;
        }
        public override void Attack()
        {
            Debug.Log("Machine Gun Tower is attacking");
        }
    }

}
