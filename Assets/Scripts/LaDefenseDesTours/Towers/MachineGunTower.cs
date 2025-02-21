using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunTower : Tower
    {
        public override string towerName { get; } = "Machine Gun Tower";
        public float attackPerSecond { get; set; } = 1.5f;

        public MachineGunTower()
        {
            currentLevel = 1;
            range = 30f;
            damage = 30f;
            cost = 80;
        }
        public override void Upgrade()
        {
            currentLevel++;

            switch (currentLevel)
            {
                case 2:
                    // 1er upgrade
                    cost += 40;
                    damage += 40f;
                    range += 5f;
                    attackPerSecond += 0.5f;
                    break;

                case 3:
                    // 2eme upgrade
                    cost += 80;
                    damage += 60f;
                    range += 10f;
                    attackPerSecond += 1f;
                    break;

                default:
                    Debug.LogError("Max upgrade level reached!");
                    break;
            }

            Debug.Log($"Machine Gun Tower upgraded to level {currentLevel}. New Stats - Damage: {damage}, Range: {range}, Cost: {cost}, Attack per second: {attackPerSecond}");
        }

        public override void Attack()
        {
            Debug.Log("Machine Gun Tower is attacking");
        }
    }
}
