using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunTower : Tower
    {
        public override string  towerName { get; } = "Machine Gun Tower";
        public override float range { get; set; } = 30f;
        public override float damage {get; set; } = 30f;
        public override int cost { get; set; } = 80;
        public float attackPerSecond { get; set; } = 1.5f;

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
