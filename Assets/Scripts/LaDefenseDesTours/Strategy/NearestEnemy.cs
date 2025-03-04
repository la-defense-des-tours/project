using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace LaDefenseDesTours.Strategy
{
    public class NearestEnemy : IStrategy
    {
        private readonly string ENEMY_TAG = "Enemy";
        public Transform SelectTarget(Enemy[] enemies, Vector3 towerPosition, float range)
        {
            float shortestDistance = Mathf.Infinity;
            Enemy nearest = null;
            foreach (Enemy enemy in enemies)
            {
                float distance = Vector3.Distance(towerPosition, enemy.transform.position);
                if (distance < shortestDistance && distance <= range)
                {
                    shortestDistance = distance;
                    nearest = enemy;
                }
            }
            
            return nearest != null && nearest.gameObject.CompareTag(ENEMY_TAG) ? nearest.transform : null;
        }
    }
}