using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserTower : Tower
    {
        public override string towerName { get; } = "Laser Tower";
        public override float range { get; set; } = 50f;
        public override float damage { get; set; } = 0.2f;
        public override int cost { get; set; } = 100;
        public float damageOverTime { get; set; } = 3f;

        public override void Upgrade()
        {
            currentLevel++;

            switch (currentLevel)
            {
                case 1:
                    // 1er upgrade
                    cost += 50;
                    damageOverTime += 1.5f;
                    damage += 0.3f;
                    range += 5f;
                    break;

                case 2:
                    // 2eme upgrade
                    cost += 100;
                    damageOverTime += 2.5f;
                    damage += 1f;
                    range += 10f;
                    break;

                default:
                    Debug.LogError("Max upgrade level reached!");
                    break;
            }

            Debug.Log($"Laser Tower upgraded to level {currentLevel}. New Stats - Damage: {damage}, Range: {range}, Cost: {cost}, Damage Over Time: {damageOverTime}");
        }

        public override void Attack()
        {
            Debug.Log("Laser Tower is attacking");
        }
    }
}
