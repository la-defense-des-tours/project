using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace LaDefenseDesTours.Strategy
{
    public class NearestBase : IStrategy
    {
        private readonly Vector3 basePosition;
        private readonly string ENEMY_TAG = "Enemy";

        public NearestBase(Vector3 basePos)
        {
            basePosition = basePos;
        }

        public Transform SelectTarget(Enemy[] enemies, Vector3 towerPosition, float range)
        {
            Enemy selected = null;
            float shortestDistanceToBase = Mathf.Infinity;
            foreach (Enemy enemy in enemies)
            {
                if (Vector3.Distance(towerPosition, enemy.transform.position) <= range)
                {
                    float distanceToBase = Vector3.Distance(enemy.transform.position, basePosition);
                    if (distanceToBase < shortestDistanceToBase)
                    {
                        shortestDistanceToBase = distanceToBase;
                        selected = enemy;
                    }
                }
            }

            return selected != null && selected.gameObject.CompareTag(ENEMY_TAG) ? selected.transform : null;
        }
    }
}