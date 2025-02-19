using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunTower : Tower
    {
        public override int cost { get; set; } = 80;
        public override string towerName { get; } = "Machine Gun Tower";
        private float attackPerSecond { get; set; }
        public override float range { get; } = 30f;
        public override float damage { get; } = 30f;

        public override void Upgrade()
        {
            currentLevel++;

            switch (currentLevel)
            {
                case 1:
                    // 1er upgrade
                    health += 40;
                    cost += 40;
                    damage += 40f;
                    range += 5f;
                    attackPerSecond += 0.5f;
                    break;

                case 2:
                    // 2eme upgrade
                    health += 80;
                    cost += 80;
                    damage += 60f;
                    range += 10f;
                    attackPerSecond += 1f;
                    break;

                default:
                    Debug.LogError("Max upgrade level reached!");
                    break;
            }

            Debug.Log($"Machine Gun Tower upgraded to level {currentLevel}. New Stats - Health: {health}, Damage: {damage}, Range: {range}, Cost: {cost}, Attack per second: {attackPerSecond}");
        }

        public override void Attack()
        {
            Debug.Log("Machine Gun Tower is attacking");
        }
    }
}
