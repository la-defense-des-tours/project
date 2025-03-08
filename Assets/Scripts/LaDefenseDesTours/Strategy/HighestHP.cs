using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace LaDefenseDesTours.Strategy
{
    public class HighestHP : IStrategy
    {
        private readonly string ENEMY_TAG = "Enemy";
        public string GetStrategyName()
        {
            return "Highest Hp";
        }

        public Enemy SelectTarget(Enemy[] enemies, Vector3 towerPosition, float range)
        {
            Enemy selected = null;
            float highestHP = 0;

            foreach (Enemy enemy in enemies)
            {
                float distance = Vector3.Distance(towerPosition, enemy.transform.position);
                float enemyHP = enemy.maxHealth;
                if (distance <= range && enemyHP > highestHP)
                {
                    highestHP = enemyHP;
                    selected = enemy;
                }
            }

            return selected != null && selected.gameObject.CompareTag(ENEMY_TAG) ? selected : null;
        }
    }
}