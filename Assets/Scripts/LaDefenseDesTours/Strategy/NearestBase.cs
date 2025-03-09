using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using LaDefenseDesTours.Enemies;
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

        public Enemy SelectTarget(Enemy[] enemies, Vector3 towerPosition, float range)
        {

            if (enemies == null || enemies.Length == 0)
                return null;

            Enemy selected = null;
            float shortestDistanceToBase = Mathf.Infinity;
            Vector3 basePosition = LevelManager.instance.GetEnemyEndPoint();

            foreach (Enemy enemy in enemies)
            {
                if (enemy == null || !enemy.gameObject.activeInHierarchy || !enemy.CompareTag(ENEMY_TAG))
                    continue;

                float distanceToEnemy = Vector3.Distance(towerPosition, enemy.transform.position);

                if (distanceToEnemy > range)
                    continue;

                float distanceToBase = Vector3.Distance(enemy.transform.position, basePosition);
                if (distanceToBase < shortestDistanceToBase)
                {
                    shortestDistanceToBase = distanceToBase;
                    selected = enemy;
                }
            }

            return selected;
        }
    }
}