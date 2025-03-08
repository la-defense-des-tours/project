using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace LaDefenseDesTours.Strategy
{
    public class NearestEnemy : IStrategy
    {
        
        private readonly string ENEMY_TAG = "Enemy";
        public string GetStrategyName()
        {
            return "Nearest Enemy";
        }
        public Enemy SelectTarget(Enemy[] enemies, Vector3 towerPosition, float range)
        {
            float shortestDistance = Mathf.Infinity;
            Enemy selected = null;

            foreach (Enemy enemy in enemies)
            {
                float distance = Vector3.Distance(towerPosition, enemy.transform.position);
                if (distance < shortestDistance && distance <= range)
                {
                    shortestDistance = distance;
                    selected = enemy;
                }
            }

            return selected != null && selected.gameObject.CompareTag(ENEMY_TAG) ? selected : null;
        }
    }
}