using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserTower : Tower
    {
        public override string towerName { get; } = "Laser Tower";
        public float damageOverTime { get; set; } = 3f;

        public LaserTower()
        {
            currentLevel = 1;
            range = 50f;
            damage = 0.25f;
            cost = 100;
        }
        public override void Upgrade()
        {
            currentLevel++;

            switch (currentLevel)
            {
                case 2:
                    // 1er upgrade
                    cost += 50;
                    damageOverTime += 1.5f;
                    damage += 0.3f;
                    range += 5f;
                    break;

                case 3:
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
