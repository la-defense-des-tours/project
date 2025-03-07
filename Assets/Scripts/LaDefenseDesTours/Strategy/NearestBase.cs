using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using UnityEngine;

namespace LaDefenseDesTours.Strategy
{
    public class NearestBase : IStrategy
    {
        private readonly string ENEMY_TAG = "Enemy";
        public string GetStrategyName()
        {
            return "Nearest Base";
        }

        public Transform SelectTarget(Enemy[] enemies, Vector3 towerPosition, float range)
        {
            Enemy selected = null;
            float shortestDistanceToBase = Mathf.Infinity;
            
            foreach (Enemy enemy in enemies)
            {
                float distance = Vector3.Distance(towerPosition, enemy.transform.position);
                float distanceToBase = Vector3.Distance(enemy.transform.position, LevelManager.instance.GetEnemyEndPoint());
                if (distance <= range && distanceToBase < shortestDistanceToBase)
                {
                    shortestDistanceToBase = distanceToBase;
                    selected = enemy;
                }
            }

            return selected != null && selected.gameObject.CompareTag(ENEMY_TAG) ? selected.transform : null;
        }
    }
}