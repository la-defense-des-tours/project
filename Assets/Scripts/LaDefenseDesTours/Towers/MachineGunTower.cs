using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunTower : MonoBehaviour, Tower
    {
        private float range { get; set; }
        private int currentLevel { get; set; }
        private float damage { get; set; }
        private float fireRate { get; set; }
        private int cost { get; set; }
        private int health { get; set; }
        private int upgradeCost { get; set; }
        private float upgradeDamage { get; set; }
        private float upgradeFireRate { get; set; }
        private float upgradeRange { get; set; }
        private int sellValue { get; set; }
        private int upgradeSellValue { get; set; }
        private float attackPerSecond { get; set; }
        public void Upgrade()
        {
            currentLevel++;
            health += 10;
            cost += 10;
            attackPerSecond += 0.5f;
        }
        public void Attack()
        {
            Debug.Log("Machine Gun Tower is attacking");
        }
    }
}
