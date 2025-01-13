using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Enemies;

namespace Assets.Scripts.LaDefenseDesTours.Factories
{
    public class FlyingEnemyFactory : EnemyFactory
    {
        [SerializeField] private FlyingEnemy flyingEnemy;
        public override Enemy CreateEnemy()
        {
            Notify();
            GameObject instance = Instantiate(flyingEnemy.gameObject, transform.position, Quaternion.identity);
            return instance.GetComponent<FlyingEnemy>();
        }
        public override void Notify()
        {
            Debug.Log("Flying enemy created!");
        }
    }
}