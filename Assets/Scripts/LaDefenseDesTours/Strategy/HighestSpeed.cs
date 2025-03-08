using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace LaDefenseDesTours.Strategy
{
    public class HighestSpeed : IStrategy
    {
        private readonly string ENEMY_TAG = "Enemy";
        public string GetStrategyName()
        {
            return "Highest Speed";
        }

        public Enemy SelectTarget(Enemy[] enemies, Vector3 towerPosition, float range)
        {
            Enemy selected = null;
            float highestSpeed = float.MinValue;
            foreach (Enemy enemy in enemies)
            {
                float distance = Vector3.Distance(towerPosition, enemy.transform.position);
                float enemySpeed = enemy.GetSpeed();

                if (distance <= range && enemySpeed > highestSpeed)
                {
                    highestSpeed = enemySpeed;
                    selected = enemy;
                }
            }

            return selected && selected.gameObject.CompareTag(ENEMY_TAG) ? selected : null;
        }
    }
}