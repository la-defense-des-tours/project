using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserTower : Tower
    {
        public override string towerName { get; } = "Laser Tower";
        public float damageOverTime { get; set; }
        public override float range { get; set; } = 50f;
        public override float damage { get; set; } = 0.05f;

        public override void Upgrade()
        {
            currentLevel++;

            switch (currentLevel)
            {
                case 1:
                    // 1er upgrade
                    health += 50;
                    cost += 50;
                    damageOverTime += 1.5f;
                    damage += 0.1f;
                    range += 5f;
                    break;

                case 2:
                    // 2eme upgrade
                    health += 100;
                    cost += 100;
                    damageOverTime += 2.5f;
                    damage += 0.2f;
                    range += 10f;
                    break;

                default:
                    Debug.LogError("Max upgrade level reached!");
                    break;
            }

            Debug.Log($"Laser Tower upgraded to level {currentLevel}. New Stats - Health: {health}, Damage: {damage}, Range: {range}, Cost: {cost}, Damage Over Time: {damageOverTime}");
        }

        public override void Attack()
        {
            Debug.Log("Laser Tower is attacking");
        }
    }
}
